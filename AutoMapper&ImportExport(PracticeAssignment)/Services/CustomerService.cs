using AutoMapper;
using E_Commerce.Common;
using E_Commerce.CosmosDB;
using E_Commerce.DTO;
using E_Commerce.Entity;
using E_Commerce.Interface;
using System.Net;

namespace E_Commerce.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICosmosDBService _cosmosDBService;
        public readonly IMapper _mapper;

        public CustomerService(ICosmosDBService cosmosDBService,IMapper mapper)
        {
            _cosmosDBService = cosmosDBService;
            _mapper = mapper;
        }
        public async Task<CustomerDTO> AddCustomer(CustomerDTO customerDTO)
        {
           /* CustomerEntity customer = new CustomerEntity
            {
                Name = customerDTO.Name,
                Email = customerDTO.Email,
                Address = customerDTO.Address,
                PhoneNumber = customerDTO.PhoneNumber,
            };*/
           var customer = _mapper.Map<CustomerEntity>(customerDTO);


            customer.Initalize(true, Credentials.CustomerDocumentType, "Shruti", "Shruti");

            var response = await _cosmosDBService.AddCustomer(customer);

           /* CustomerDTO responseDTO = new CustomerDTO
            {
                UId = response.UId,
                Name = response.Name,
                Email = response.Email,
                Address = response.Address,
                PhoneNumber = response.PhoneNumber,
            };*/

            var responseDTO = _mapper.Map<CustomerDTO>(response);

            return responseDTO;


        }

        public async Task<CustomerDTO> GetCustomerByUId(string UId)
        {
            var response = await _cosmosDBService.GetCustomerByUId(UId);


            /* CustomerDTO customerDTO = new CustomerDTO
             {
                 UId = response.UId,
                 Name = response.Name,
                 Email = response.Email,
                 Address = response.Address,
                 PhoneNumber = response.PhoneNumber,
             };*/

            var customerDTO = _mapper.Map<CustomerDTO>(response);
            return customerDTO;



        }

        public async Task<List<CustomerDTO>> GetAllCustomer()
        {
            var customers = await _cosmosDBService.GetAllCustomer();


            var customerList = new List<CustomerDTO>();

            foreach (var customer in customers)
            {
               /* var customerDTO = new CustomerDTO();
                customerDTO.UId = customer.UId;
                customerDTO.Name = customer.Name;
                customerDTO.Email = customer.Email;
                customerDTO.Address = customer.Address;
                customerDTO.PhoneNumber = customer.PhoneNumber;*/

                var customerDTO=_mapper.Map<CustomerDTO>(customer);

                customerList.Add(customerDTO);
            }
            return customerList;
        }

        public async Task<CustomerDTO> UpdateCustomer(CustomerDTO customerDTO)
        {
            var existingCustomer = await _cosmosDBService.GetCustomerByUId(customerDTO.UId);
            existingCustomer.Active = true;
            existingCustomer.Archived = false;


            await _cosmosDBService.ReplaceAsync(existingCustomer);


            existingCustomer.Initalize(false, Credentials.CustomerDocumentType, "shruti", "shruti");

            var updatedCustomerEntity = _mapper.Map<CustomerEntity>(customerDTO);


            _mapper.Map(updatedCustomerEntity, existingCustomer);

            /* existingCustomer.Name = customerDTO.Name;
             existingCustomer.Email = customerDTO.Email;
             existingCustomer.Address = customerDTO.Address;
             existingCustomer.PhoneNumber = customerDTO.PhoneNumber;*/
            var response = await _cosmosDBService.AddCustomer(existingCustomer);

            /*var responseDTO = new CustomerDTO
            {
                UId = customerDTO.UId,
                Name = customerDTO.Name,
                Email = customerDTO.Email,
                Address = customerDTO.Address,
                PhoneNumber = customerDTO.PhoneNumber,

            };*/

            var responseDTO = _mapper.Map<CustomerDTO>(response);
            return responseDTO;


        }

        public async Task<string> DeleteCustomer(string uId)
        {
            var customer = await _cosmosDBService.GetCustomerByUId(uId);
            customer.Active = false;
            customer.Archived = true;

            await _cosmosDBService.ReplaceAsync(customer);

            customer.Initalize(false, Credentials.CustomerDocumentType, "shruti", "shruti");
            customer.Archived = true;

            var response = await _cosmosDBService.AddCustomer(customer);
            return "Record Deleted";

        }
    }
}
