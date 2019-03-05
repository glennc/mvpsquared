using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace OrderApi.Models
{
    public class Order
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [Required]
        [JsonProperty(PropertyName = "accountId")]
        public string AccountId { get; set; }
        
        [JsonProperty(PropertyName = "cart")]
        public OrderItem[] Cart { get; set; }
    }

    public class OrderItem
    {
        [JsonProperty(PropertyName = "itemId")]
        public string CatalogItemId { get; set; }

        [Range(1, 1000)]
        [JsonProperty(PropertyName = "quantity")]
        public int Quantity { get; set; }
    }
}
