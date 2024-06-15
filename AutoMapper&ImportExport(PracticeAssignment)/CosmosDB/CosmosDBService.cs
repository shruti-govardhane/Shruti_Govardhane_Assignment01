using E_Commerce.Common;
using E_Commerce.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using System.ComponentModel;
using Container = Microsoft.Azure.Cosmos.Container;

namespace E_Commerce.CosmosDB
{
    public class CosmosDBService : ICosmosDBService
    {
        public readonly CosmosClient cosmosClient;
        private readonly Container container;

        public CosmosDBService()
        {
            cosmosClient = new CosmosClient(Credentials.CosmosEndPoint, Credentials.PrimaryKey);
            container= cosmosClient.GetContainer(Credentials.databaseName,Credentials.containerName);
        }

        public async Task<ProductEntity> AddProduct(ProductEntity product)
        {
            var response =await container.CreateItemAsync(product);
            return response;
        }
        public async Task<ProductEntity> GetProductByUId(string uId)
        {
            var response = container.GetItemLinqQueryable<ProductEntity>(true).Where(a => a.UId == uId && a.DocumentType == Credentials.ProductDocumentType && !a.Archived && a.Active).FirstOrDefault();
            return response;
        }

        public async Task<List<ProductEntity>> GetProductByCategory(string category)
        {
            var response = container.GetItemLinqQueryable<ProductEntity>(true).Where(a => a.Category == category && a.DocumentType == Credentials.ProductDocumentType).ToList();
            return response;

        }

        public async Task<List<ProductEntity>> GetAllProduct()
        {
            var response = container.GetItemLinqQueryable<ProductEntity>(true).Where(a =>a.DocumentType== Credentials.ProductDocumentType && a.Archived== false && a.Active==true).ToList(); return response;
            return response;
        }
        public async Task ReplaceAsync(dynamic entity)
        {
            var response = await container.ReplaceItemAsync(entity, entity.Id);
        }


        //Cutomer

        public async Task<CustomerEntity> AddCustomer(CustomerEntity customer)
        {
            var response = await container.CreateItemAsync(customer);
            return response;
        }
        public async Task<CustomerEntity> GetCustomerByUId(string uId)
        {
            var response = container.GetItemLinqQueryable<CustomerEntity>(true).Where(a => a.UId == uId && a.DocumentType == Credentials.CustomerDocumentType && !a.Archived && a.Active).FirstOrDefault();
            return response;
        }

        public async Task<List<CustomerEntity>> GetAllCustomer()

        {
            var response = container.GetItemLinqQueryable<CustomerEntity>(true).Where(a => a.DocumentType == Credentials.CustomerDocumentType && a.Archived == false && a.Active == true).ToList(); return response;
            return response;
        }
        /*public async Task ReplaceAsync(dynamic entity)
        {
            var response = await container.ReplaceItemAsync(entity, entity.Id);
        }*/

       /* public async Task<List<ProductEntity>> GetProductByCustomerID(string uId)
        {
            var response = container.GetItemLinqQueryable<ProductEntity>(true).Where(a => a.UId== uId && a.DocumentType == Credentials.ProductDocumentType).ToList();
            return response;

        }*/


        //Orderr 

        public async Task<OrderEntity> AddOrder(OrderEntity order)
        {
            var response = await container.CreateItemAsync(order);
            return response;
        }

        public async Task<OrderEntity> GetOrderByUId(string uId)
        {
            var response = container.GetItemLinqQueryable<OrderEntity>(true).Where(a => a.UId == uId && a.DocumentType == Credentials.OrderDocumentType).FirstOrDefault();
            return response;
        }

    }
}
