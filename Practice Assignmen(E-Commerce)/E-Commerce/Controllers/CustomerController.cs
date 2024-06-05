using E_Commerce.DTO;
using E_Commerce.Interface;
using E_Commerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController:Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
      

        [HttpPost]
        public async Task<CustomerDTO> AddCustomer(CustomerDTO customerDTO)
        {
            var response = await _customerService.AddCustomer(customerDTO);
            return response;

        }
        [HttpPost]
        public async Task<CustomerDTO> GetCustomerByUId(string UId)
        {
            var response = await _customerService.GetCustomerByUId(UId);
            return response;
        }

        [HttpPost]
        public async Task<List<CustomerDTO>> GetAllCustomer()
        {
            var response = await _customerService.GetAllCustomer();
            return response;
        }

        [HttpPost]
        public async Task<CustomerDTO> UpdateCustomer(CustomerDTO customerDTO)
        {
            var response = await _customerService.UpdateCustomer(customerDTO);
            return response;
        }

        [HttpPost]
        public async Task<string> DeleteCustomer(string UId)
        {
            var response = await _customerService.DeleteCustomer(UId);
            return response;
        }
    }
}
