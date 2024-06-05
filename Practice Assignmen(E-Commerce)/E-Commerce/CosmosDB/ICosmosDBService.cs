using E_Commerce.DTO;
using E_Commerce.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Commerce.CosmosDB
{
    public interface ICosmosDBService
    {
        Task<ProductEntity> AddProduct(ProductEntity product);
        Task<ProductEntity> GetProductByUId(string uId);

        Task<List<ProductEntity>> GetProductByCategory(string category);
        Task<List<ProductEntity>> GetAllProduct();

        Task ReplaceAsync(dynamic entity);


        Task<CustomerEntity> AddCustomer(CustomerEntity customer);

        Task<CustomerEntity> GetCustomerByUId(string uId);
        Task<List<CustomerEntity>> GetAllCustomer();





        Task<OrderEntity> AddOrder(OrderEntity order);

        Task<OrderEntity> GetOrderByUId(string uId);

    }

}
