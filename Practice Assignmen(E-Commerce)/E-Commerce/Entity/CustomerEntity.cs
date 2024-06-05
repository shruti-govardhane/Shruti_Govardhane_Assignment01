using E_Commerce.Common;
using Newtonsoft.Json;

namespace E_Commerce.Entity
{
    public class CustomerEntity:BaseEntity
    {

        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "address", NullValueHandling = NullValueHandling.Ignore)]
        public string Address { get; set; }

        [JsonProperty(PropertyName = "phoneNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string PhoneNumber { get; set; }

        internal float Sum(Func<object, object> value)
        {
            throw new NotImplementedException();
        }
    }
}
