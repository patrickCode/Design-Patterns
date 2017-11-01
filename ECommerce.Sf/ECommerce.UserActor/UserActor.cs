using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.ServiceFabric.Actors.Client;
using ECommerce.UserActor.Interfaces;

namespace ECommerce.UserActor
{
    /// <remarks>
    /// This class represents an actor.
    /// Every ActorID maps to an instance of this class.
    /// The StatePersistence attribute determines persistence and replication of actor state:
    ///  - Persisted: State is written to disk and replicated.
    ///  - Volatile: State is kept in memory only and replicated.
    ///  - None: State is kept in memory only and not replicated.
    /// </remarks>
    [StatePersistence(StatePersistence.Persisted)]
    internal class UserActor : Actor, IUserActor
    {
        /// <summary>
        /// Initializes a new instance of UserActor
        /// </summary>
        /// <param name="actorService">The Microsoft.ServiceFabric.Actors.Runtime.ActorService that will host this actor instance.</param>
        /// <param name="actorId">The Microsoft.ServiceFabric.Actors.ActorId for this actor instance.</param>
        public UserActor(ActorService actorService, ActorId actorId)
            : base(actorService, actorId)
        {
        }

        public async Task AddProductToCart(Guid productId, int quantity)
        {
            await StateManager.AddOrUpdateStateAsync(stateName: productId.ToString(),
                addValue: quantity,
                updateValueFactory: (id, oldQuantity) => quantity);
        }

        public async Task ClearCart()
        {
            var productIds = await StateManager.GetStateNamesAsync();

            foreach(var productId in productIds)
            {
                await StateManager.RemoveStateAsync(productId);
            }
        }

        public async Task DeleteProductFromCart(Guid productId)
        {
            await StateManager.TryRemoveStateAsync(productId.ToString());
        }

        public async Task<Dictionary<Guid, int>> GetCart()
        {
            var allProducts = await StateManager.GetStateNamesAsync();
            var result = new Dictionary<Guid, int>();

            foreach (var productId in allProducts)
            {
                var quantity = await StateManager.GetStateAsync<int>(productId);
                result.Add(Guid.Parse(productId), quantity);
            }

            return result;
        }

        public async Task UpdateProductInCart(Guid productId, int quantity)
        {
            await AddProductToCart(productId, quantity);
        }

        /// <summary>
        /// This method is called whenever an actor is activated.
        /// An actor is activated the first time any of its methods are invoked.
        /// </summary>
        protected override async Task OnActivateAsync()
        {
            await Task.Run(() =>
            {
                ActorEventSource.Current.ActorMessage(this, "Actor activated.");
            });

            // The StateManager is this actor's private state store.
            // Data stored in the StateManager will be replicated for high-availability for actors that use volatile or persisted state storage.
            // Any serializable object can be saved in the StateManager.
            // For more information, see https://aka.ms/servicefabricactorsstateserialization

            //return this.StateManager.TryAddStateAsync("count", 0);
        }


        #region Default methods <Not Needed>
        /// <summary>
        /// TODO: Replace with your own actor method.
        /// </summary>
        /// <returns></returns>
        Task<int> GetCountAsync(CancellationToken cancellationToken)
        {
            return this.StateManager.GetStateAsync<int>("count", cancellationToken);
        }

        /// <summary>
        /// TODO: Replace with your own actor method.
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        Task SetCountAsync(int count, CancellationToken cancellationToken)
        {
            // Requests are not guaranteed to be processed in order nor at most once.
            // The update function here verifies that the incoming count is greater than the current count to preserve order.
            return this.StateManager.AddOrUpdateStateAsync("count", count, (key, value) => count > value ? count : value, cancellationToken);
        }
        #endregion
    }
}
