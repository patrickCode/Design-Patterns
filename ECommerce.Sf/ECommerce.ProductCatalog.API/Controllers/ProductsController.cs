using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ECommerce.ProductCatalog.Model;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using System;
using Microsoft.ServiceFabric.Services.Client;
using ECommerce.ProductCatalog.API.Models;
using System.Threading.Tasks;
using System.Linq;

namespace ECommerce.ProductCatalog.API.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private IProductCatalogService _productCatalogService;
        public ProductsController()
        {
            _productCatalogService = ServiceProxy.Create<IProductCatalogService>(
                serviceUri: new Uri("fabric:/ECommerce.Sf/ECommerce.ProductCatalog"), 
                partitionKey: new ServicePartitionKey(0),
                listenerName: "ProductCatalog.Remoting.Listener");
        }

        // GET api/products
        [HttpGet]
        public async Task<IEnumerable<ProductDto>> Get()
        {
            try
            {
                var products = await _productCatalogService.GetAllProducts();

                return products.Select(product => new ProductDto()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    IsAvailable = product.Availability > 0,
                    Price = product.Price
                }).ToList();
            }
            catch (Exception error)
            {
                throw error;
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task Post([FromBody]ProductDto product)
        {
            await _productCatalogService.AddProduct(new Product()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Availability = product.IsAvailable ? 1 : 0,
                Price = product.Price
            });
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
