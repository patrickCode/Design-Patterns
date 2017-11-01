using ECommerce.ProductCatalog.API.Models;
using ECommerce.UserActor.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.ProductCatalog.API.Controllers
{
    [Route("api/[controller]")]
    public class CartsController : Controller
    {
        private IUserActor _userActor;

        public CartsController()
        {
        }

        [HttpGet]
        public async Task<CartDto> GetCart([FromQuery] string userId)
        {
            try
            {
                CreateActor(userId);
                var cart = await _userActor.GetCart();

                return new CartDto()
                {
                    UserId = userId,
                    Items = cart.Select(c => new CartItem()
                    {
                        ProductId = c.Key.ToString(),
                        Quantity = c.Value
                    }).ToArray()
                };
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        [HttpPost]
        public async Task AddItem([FromQuery] string userId, [FromBody] CartItem item)
        {
            CreateActor(userId);
            await _userActor.AddProductToCart(Guid.Parse(item.ProductId), item.Quantity);
        }

        [HttpPut]
        public async Task ModifyItem([FromQuery] string userId, [FromBody] CartItem item)
        {
            CreateActor(userId);
            await _userActor.UpdateProductInCart(Guid.Parse(item.ProductId), item.Quantity);
        }

        [HttpDelete]
        [Route("/items")]
        public async Task DeleteItem([FromQuery] string userId, [FromQuery] string productId)
        {
            CreateActor(userId);
            await _userActor.DeleteProductFromCart(Guid.Parse(productId));
        }

        [HttpDelete]
        public async Task DeleteCart([FromQuery] string userId)
        {
            CreateActor(userId);
            await _userActor.ClearCart();
        }

        private void CreateActor(string userId)
        {
            _userActor = ActorProxy.Create<IUserActor>(
                actorId: new ActorId(userId),
                serviceUri: new Uri("fabric:/ECommerce.Sf/UserActorService"));
        }
    }
}