using ShopMate.BLL.DTO.AdminDto;

namespace ShopMate.BLL.Service.Abstraction
{
    public interface IOrderService
    {
        Task<List<OrderDto>> GetAllOrdersAsync();
        Task<OrderDetailsDto> GetOrderByIdAsync(int id);
        Task<bool> UpdateOrderStatusAsync(int orderId, string newStatus);
    }
}
