using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ECommerce.ProductCatalog.API.Models
{
    public class CheckoutDto
    {
        [JsonProperty("products")]
        public List<CheckoutItem> Products { get; set; }

        [JsonProperty("totalPrice")]
        public double TotalPrice { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }
    }

    public class CheckoutItem
    {
        [JsonProperty("productId")]
        public Guid ProductId { get; set; }

        [JsonProperty("productName")]
        public string ProductName { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("price")]
        public double Price { get; set; }
    }
}