using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using ECommerce.CheckoutService.Model;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using ECommerce.UserActor.Interfaces;
using Microsoft.ServiceFabric.Actors.Client;
using Microsoft.ServiceFabric.Actors;
using ECommerce.ProductCatalog.Model;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Microsoft.ServiceFabric.Services.Client;

namespace ECommerce.CheckoutService
{
    /// <summary>
    /// An instance of this class is created for each service replica by the Service Fabric runtime.
    /// </summary>
    internal sealed class CheckoutService : StatefulService, ICheckoutService
    {
        public CheckoutService(StatefulServiceContext context)
            : base(context)
        { }

        public async Task<CheckoutSummary> Checkout(string userId)
        {
            var result = new CheckoutSummary()
            {
                Date = DateTime.UtcNow,
                Products = new List<CheckoutProduct>()
            };

            IUserActor actor = CreateUserActor(userId);
            var cart = await actor.GetCart();

            IProductCatalogService productCatalogService = GetProductCatalogService();
            var products = await productCatalogService.GetAllProducts();

            var totalCost = 0.0;
            foreach (var item in cart)
            {
                var productId = item.Key;
                var quantity = item.Value;
                var product = products.FirstOrDefault(p => p.Id == productId);
                if (product != null)
                {
                    result.Products.Add(new CheckoutProduct()
                    {
                        Product = product,
                        Quantity = quantity,
                        Price = product.Price * quantity
                    });
                    totalCost += (product.Price * quantity);
                }
            }
            result.TotalPrice = totalCost;

            //Clearing the cart since user has checkout all the products and add to history
            await actor.ClearCart();
            await AddToHistory(result);
            return result;
        }

        public async Task<IEnumerable<CheckoutSummary>> GetOrderHistory(string userId)
        {
            var history = await StateManager.GetOrAddAsync<IReliableDictionary<DateTime, CheckoutSummary>>("history");
            var result = new List<CheckoutSummary>();

            using (var tx = StateManager.CreateTransaction())
            {
                var checkouts = await history.CreateEnumerableAsync(tx, EnumerationMode.Ordered);
                using (var enumerator = checkouts.GetAsyncEnumerator())
                {
                    while (await enumerator.MoveNextAsync(CancellationToken.None))
                    {
                        var currentSummary = enumerator.Current;
                        result.Add(currentSummary.Value);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Optional override to create listeners (e.g., HTTP, Service Remoting, WCF, etc.) for this service replica to handle client or user requests.
        /// </summary>
        /// <remarks>
        /// For more information on service communication, see https://aka.ms/servicefabricservicecommunication
        /// </remarks>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        {
            return new ServiceReplicaListener[]
            {
                new ServiceReplicaListener(serviceContext => this.CreateServiceRemotingListener(serviceContext))
            };
        }

        private IUserActor CreateUserActor(string userId)
        {
            return ActorProxy.Create<IUserActor>(
                actorId: new ActorId(userId),
                serviceUri: new Uri("fabric:/ECommerce.Sf/UserActorService"));
        }

        private async Task AddToHistory(CheckoutSummary summary)
        {
            var history = await StateManager.GetOrAddAsync<IReliableDictionary<DateTime, CheckoutSummary>>("history");

            using (var tx = StateManager.CreateTransaction())
            {
                await history.AddAsync(tx, summary.Date, summary);
                await tx.CommitAsync();
            }
        }

        private IProductCatalogService GetProductCatalogService()
        {
            return ServiceProxy.Create<IProductCatalogService>(
                serviceUri: new Uri("fabric:/ECommerce.Sf/ECommerce.ProductCatalog"),
                partitionKey: new ServicePartitionKey(0),
                listenerName: "ProductCatalog.Remoting.Listener");
        }
    }
}
