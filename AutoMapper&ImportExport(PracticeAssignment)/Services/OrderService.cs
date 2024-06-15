using E_Commerce.Common;
using E_Commerce.CosmosDB;
using E_Commerce.DTO;
using E_Commerce.Entity;
using E_Commerce.Interface;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Services
{
    public class OrderService : IOrderService
    {



        private readonly ICosmosDBService _cosmosDBService;

        public OrderService(ICosmosDBService cosmosDBService)
        {
            _cosmosDBService = cosmosDBService;
        }

        public async Task<OrderDTO> AddOrder(OrderDTO orderDTO)
        {


            List<ProductListDTO> products = orderDTO.Products;
            float totalAmount = 0;
            Console.WriteLine(products);

            for (int i = 0; i < products.Count; i++) {
                {
                    var product = await _cosmosDBService.GetProductByUId(products[i].productUID);
                    float perProductAmt = product.Price * products[i].quantity;
                    totalAmount = totalAmount + perProductAmt;
                }
            }
            OrderEntity order = new OrderEntity{
                TotalAmount = totalAmount,
                Id = Guid.NewGuid().ToString(),
                UId = orderDTO.UId,
                DocumentType = Credentials.OrderDocumentType,
                Status = orderDTO.Status,
                OrderDate = DateTime.Now.ToString(),
               CreatedOn = DateTime.Now,
               Products = products
            };
            
            Console.WriteLine("ORDER :-" + order.Id + " "+ order.UId);

            var savedOrder = await _cosmosDBService.AddOrder(order);

            OrderDTO response = new OrderDTO();
            {
                response.UId = savedOrder.UId;
                response.Status = savedOrder.Status;
                response.TotalAmount = savedOrder.TotalAmount;
                response.Products = savedOrder.Products;
              
            }

            return response;

        }

        public async Task<OrderDTO> GetOrderByUId(string UId)
        {
            var response = await _cosmosDBService.GetOrderByUId(UId);


            OrderDTO orderDTO = new OrderDTO
            {
                UId = response.UId,
                TotalAmount = response.TotalAmount,
                Status = response.Status,
                Products = response.Products,
                
            };
            return orderDTO;



        }
    }
}
