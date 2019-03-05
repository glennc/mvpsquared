using System;
using Newtonsoft.Json;

namespace OrderWorker.Models
{
    public class Order
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "accountId")]
        public string AccountId { get; set; }
        
        [JsonProperty(PropertyName = "cart")]
        public OrderItem[] Cart { get; set; }
    }

    public class OrderItem
    {
        [JsonProperty(PropertyName = "itemId")]
        public string CatalogItemId { get; set; }

        [JsonProperty(PropertyName = "quantity")]
        public int Quantity { get; set; }
    }
}
