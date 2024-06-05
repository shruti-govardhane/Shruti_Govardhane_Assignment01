using E_Commerce.Common;
using E_Commerce.DTO;
using Newtonsoft.Json;

namespace E_Commerce.Entity
{
    public class OrderEntity:BaseEntity
    {
        

        [JsonProperty(PropertyName = "uId", NullValueHandling = NullValueHandling.Ignore)]
        public string UId { get; set; }

        [JsonProperty(PropertyName = "orderDate", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderDate { get; set; }

        [JsonProperty(PropertyName = "totalAmount", NullValueHandling = NullValueHandling.Ignore)]
        public float TotalAmount { get; set; }

        [JsonProperty(PropertyName = "status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }


        [JsonProperty(PropertyName = "products", NullValueHandling = NullValueHandling.Ignore)]
        public List<ProductListDTO> Products { get; set; }


        [JsonProperty(PropertyName = "documentType", NullValueHandling = NullValueHandling.Ignore)]
        public string DocumentType { get; set; } 


        public void  toString() {
            Console.WriteLine(Id + "" + UId);
        
        }
    }
}
