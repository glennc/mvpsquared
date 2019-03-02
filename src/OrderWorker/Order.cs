using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace OrderWorker
{
    public class Order
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [Required]
        [JsonProperty(PropertyName = "accountId")]
        public string AccountId { get; set; }

        [Range(0.01, 1000000)]
        [JsonProperty(PropertyName = "pricePerWidget")]
        public decimal PricePerWidget { get; set; }

        [Range(1, 1000)]
        [JsonProperty(PropertyName = "numberOfWidgets")]
        public int NumberOfWidgets { get; set; }
    }
}