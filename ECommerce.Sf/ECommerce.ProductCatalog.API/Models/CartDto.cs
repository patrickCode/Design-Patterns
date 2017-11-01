using Newtonsoft.Json;

namespace ECommerce.ProductCatalog.API.Models
{
    public class CartDto
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("item")]
        public CartItem[] Items { get; set; }
    }

    public class CartItem
    {
        [JsonProperty("productId")]
        public string ProductId { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }
    }
}