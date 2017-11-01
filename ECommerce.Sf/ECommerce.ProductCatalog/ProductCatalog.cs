using System;
using System.Collections.Generic;
using System.Fabric;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ServiceFabric.Services.Communication.AspNetCore;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Microsoft.ServiceFabric.Data;
using ECommerce.ProductCatalog.Spec;
using ECommerce.ProductCatalog.Model;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;

namespace ECommerce.ProductCatalog
{
    /// <summary>
    /// The FabricRuntime creates an instance of this class for each service type instance. 
    /// </summary>
    internal sealed class ProductCatalog : StatefulService, IProductCatalogService
    {
        private IProductRepository _productRepository;
        public ProductCatalog(StatefulServiceContext context)
            : base(context)
        { }

        public Task AddProduct(Product product)
        {
            return _productRepository.AddProduct(product);
        }

        public Task<IEnumerable<Product>> GetAllProducts()
        {
            return _productRepository.GetAllProducts();
        }

        /// <summary>
        /// Optional override to create listeners (like tcp, http) for this service instance.
        /// </summary>
        /// <returns>The collection of listeners.</returns>
        protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        {
            return new ServiceReplicaListener[]
            {
                //Kestrel Listener (for HTTP communication)
                new ServiceReplicaListener(serviceContext =>
                    new KestrelCommunicationListener(serviceContext, (url, listener) =>
                    {
                        ServiceEventSource.Current.ServiceMessage(serviceContext, $"Starting Kestrel on {url}");

                        return new WebHostBuilder()
                                    .UseKestrel()
                                    .ConfigureServices(
                                        services => services
                                            .AddSingleton<StatefulServiceContext>(serviceContext)
                                            .AddSingleton<IReliableStateManager>(this.StateManager))
                                    .UseContentRoot(Directory.GetCurrentDirectory())
                                    .UseStartup<Startup>()
                                    .UseServiceFabricIntegration(listener, ServiceFabricIntegrationOptions.UseUniqueServiceUrl)
                                    .UseUrls(url)
                                    .Build();
                    }), name: "ProductCatalog.Kestrel.Listener"),

                //Service Remoting Listener
                new ServiceReplicaListener(serviceContext => this.CreateServiceRemotingListener(serviceContext), name: "ProductCatalog.Remoting.Listener")
            };
        }

        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            _productRepository = new ProductRepository(this.StateManager);

            //Sample Date
            var product1 = new Product()
            {
                Id = Guid.NewGuid(),
                Name = "Product 1",
                Description = "Sample product for seeding database",
                Price = 142,
                Availability = 1
            };
            var product2 = new Product()
            {
                Id = Guid.NewGuid(),
                Name = "Product 2",
                Description = "Sample product for seeding database",
                Price = 500,
                Availability = 0
            };

            await _productRepository.AddProduct(product1);
            await _productRepository.AddProduct(product2);

            var allProducts = await _productRepository.GetAllProducts();
        }
    }
}
