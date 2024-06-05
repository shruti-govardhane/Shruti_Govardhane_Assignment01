using E_Commerce.Common;
using Newtonsoft.Json;

namespace E_Commerce.Entity
{
    public class ProductEntity :BaseEntity
    {
        

        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "price", NullValueHandling = NullValueHandling.Ignore)]
        public float Price { get; set; }

        [JsonProperty(PropertyName = "quantity", NullValueHandling = NullValueHandling.Ignore)]
        public int Quantity { get; set; }

        [JsonProperty(PropertyName = "category", NullValueHandling = NullValueHandling.Ignore)]
        public string Category { get; set; }

    }
}
