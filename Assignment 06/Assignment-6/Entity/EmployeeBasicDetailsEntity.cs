using Employee_Management_System.Common;
using Employee_Management_System.DTO;
using Newtonsoft.Json;
using System.Net;
using Employee_Management_System.Entity;

namespace Employee_Management_System.Entity
{
    public class EmployeeBasicDetailsEntity : BaseEntity
    {
        [JsonProperty(PropertyName = "salutory", NullValueHandling = NullValueHandling.Ignore)]
        public string Salutory { get; set; }

        [JsonProperty(PropertyName = "firstName", NullValueHandling = NullValueHandling.Ignore)]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "middleName", NullValueHandling = NullValueHandling.Ignore)]
        public string MiddleName { get; set; }

        [JsonProperty(PropertyName = "lastName", NullValueHandling = NullValueHandling.Ignore)]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "nickName", NullValueHandling = NullValueHandling.Ignore)]
        public string NickName { get; set; }

        [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "mobile", NullValueHandling = NullValueHandling.Ignore)]
        public string Mobile { get; set; }

        [JsonProperty(PropertyName = "employeeId", NullValueHandling = NullValueHandling.Ignore)]
        public string EmployeeId { get; set; }

        [JsonProperty(PropertyName = "role", NullValueHandling = NullValueHandling.Ignore)]
        public string Role { get; set; }

        [JsonProperty(PropertyName = "reportingManagerUId", NullValueHandling = NullValueHandling.Ignore)]
        public string ReportingManagerUId { get; set; }

        [JsonProperty(PropertyName = "reportingManagerName", NullValueHandling = NullValueHandling.Ignore)]
        public string ReportingManagerName { get; set; }

        [JsonProperty(PropertyName = "address", NullValueHandling = NullValueHandling.Ignore)]
        public Address  Address { get; set; }
        [JsonProperty(PropertyName = "status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }





    }

    public class EmployeeFilter
    {
        //Constructor
        public EmployeeFilter()
        {
            filters = new List<Filter>();
            employeeBasicDetails = new List<EmployeeBasicDetailsDTO>(); 
        }
        public int page { get; set; }

        public int pageSize { get; set; }
        public int totalCount { get; set; }

        public List<Filter> filters { get; set; }

        public List<EmployeeBasicDetailsDTO> employeeBasicDetails { get; set; }//getting response
    }
    
    public class Filter
    {
        public string FieldName { get; set; } 
        public string FieldValue { get; set; }
    }
}
