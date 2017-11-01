using ECommerce.CheckoutService.Model;
using ECommerce.ProductCatalog.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.ProductCatalog.API.Controllers
{
    [Route("api/[controller]")]
    public class CheckoutController: Controller
    {
        private readonly ICheckoutService _checkoutService;
        private static readonly Random rnd = new Random(DateTime.UtcNow.Second);

        public CheckoutController()
        {
            byte[] buf = new byte[8];
            rnd.NextBytes(buf);
            var longRand = BitConverter.ToInt64(buf, 0);
            _checkoutService = ServiceProxy.Create<ICheckoutService>(
                serviceUri: new Uri("fabric:/ECommerce.Sf/ECommerce.CheckoutService"),
                partitionKey: new ServicePartitionKey(longRand));
        }

        [HttpPost]
        public async Task<CheckoutDto> Checkout([FromQuery] string userId)
        {
            try
            {
                var checkout = await _checkoutService.Checkout(userId);
                return new CheckoutDto()
                {
                    Date = checkout.Date,
                    TotalPrice = checkout.TotalPrice,
                    Products = checkout.Products.Select(product => new CheckoutItem()
                    {
                        ProductId = product.Product.Id,
                        ProductName = product.Product.Name,
                        Price = product.Price,
                        Quantity = product.Quantity
                    }).ToList()
                };
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        [HttpGet]
        [Route("history")]
        public async Task<IEnumerable<CheckoutDto>> GetHistory(string userId)
        {
            var history = await _checkoutService.GetOrderHistory(userId);

            return history.Select(checkout => new CheckoutDto()
            {
                Date = checkout.Date,
                TotalPrice = checkout.TotalPrice,
                Products = checkout.Products.Select(product => new CheckoutItem()
                {
                    ProductId = product.Product.Id,
                    ProductName = product.Product.Name,
                    Price = product.Price,
                    Quantity = product.Quantity
                }).ToList()
            }).ToList();
        }
    }
}
