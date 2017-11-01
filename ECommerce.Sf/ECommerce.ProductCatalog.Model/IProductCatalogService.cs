using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.ServiceFabric.Services.Remoting;

namespace ECommerce.ProductCatalog.Model
{
    //Used for communicating to Product Catalog Service using Service Remoting
    public interface IProductCatalogService: IService
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task AddProduct(Product product);
    }
}