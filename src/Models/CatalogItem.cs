using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Models
{
     public class CatalogItem
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [Range(0.01, 1000000)]
        [JsonProperty(PropertyName = "price")]
        public double Price { get; set; }
    }
}