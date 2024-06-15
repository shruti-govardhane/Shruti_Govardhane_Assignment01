using E_Commerce.DTO;

namespace E_Commerce.Interface
{
    public interface IOrderService
    {

        Task<OrderDTO> AddOrder(OrderDTO orderDTO);

        Task<OrderDTO> GetOrderByUId(string UId);

      



    }
}
