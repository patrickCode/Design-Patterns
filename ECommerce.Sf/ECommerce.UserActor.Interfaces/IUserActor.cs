using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Services.Remoting;
using Microsoft.ServiceFabric.Actors.Remoting.FabricTransport;

[assembly: FabricTransportActorRemotingProvider(RemotingListener = RemotingListener.V2Listener, RemotingClient = RemotingClient.V2Client)]
namespace ECommerce.UserActor.Interfaces
{
    /// <summary>
    /// This interface defines the methods exposed by an actor.
    /// Clients use this interface to interact with the actor that implements it.
    /// </summary>
    public interface IUserActor : IActor
    {
        Task AddProductToCart(Guid productId, int quantity);
        Task DeleteProductFromCart(Guid productId);
        Task UpdateProductInCart(Guid productId, int quantity);
        Task ClearCart();
        Task<Dictionary<Guid, int>> GetCart();
    }
}