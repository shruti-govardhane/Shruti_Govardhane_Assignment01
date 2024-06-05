using E_Commerce.DTO;

namespace E_Commerce.Interface
{
    public interface ICustomerService
    {
       Task<CustomerDTO> AddCustomer(CustomerDTO customerDTO);

        Task<CustomerDTO> GetCustomerByUId(string UId);

        Task<List<CustomerDTO>> GetAllCustomer();

        Task<CustomerDTO> UpdateCustomer(CustomerDTO customerDTO);

        Task<string> DeleteCustomer(string uId);



        
    }
    
}
