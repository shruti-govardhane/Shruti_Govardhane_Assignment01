using Newtonsoft.Json;

namespace E_Commerce.DTO
{
    public class ProductListDTO
    {


        [JsonProperty(PropertyName = "productUID", NullValueHandling = NullValueHandling.Ignore)]
        public string productUID { get; set; }

        [JsonProperty(PropertyName = "quantity", NullValueHandling = NullValueHandling.Ignore)]
        public int quantity { get; set; }

    }
}
