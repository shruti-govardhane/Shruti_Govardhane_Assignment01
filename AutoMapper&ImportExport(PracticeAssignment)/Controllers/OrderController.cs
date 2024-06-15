using E_Commerce.DTO;
using E_Commerce.Interface;
using E_Commerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : Controller
    {

        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;

        }


        [HttpPost]
        public async Task<OrderDTO> AddOrder(OrderDTO orderDTO)
        {
            var response = await _orderService.AddOrder(orderDTO);
            return response;

        }


        [HttpPost]
        public async Task<OrderDTO> GetOrderUId(string UId)
        {
            var response = await _orderService.GetOrderByUId(UId);
            return response;
        }


    }
    
    
}
