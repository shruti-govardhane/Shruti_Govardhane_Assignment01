using E_Commerce.Entity;
using Newtonsoft.Json;

namespace E_Commerce.DTO
{
    public class OrderDTO
    {
        

       

        [JsonProperty(PropertyName = "uId", NullValueHandling = NullValueHandling.Ignore)]
        public string UId { get; set; }

       

        [JsonProperty(PropertyName = "totalAmount", NullValueHandling = NullValueHandling.Ignore)]
        public float TotalAmount { get; set; }

        [JsonProperty(PropertyName = "status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }


        [JsonProperty(PropertyName = "products", NullValueHandling = NullValueHandling.Ignore)]
        public List<ProductListDTO> Products { get; set; }
    }
}

