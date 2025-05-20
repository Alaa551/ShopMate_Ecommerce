using ShopMate.BLL.DTO.AdminDto;
using ShopMate.BLL.Mapping;
using ShopMate.BLL.Service.Abstraction;
using ShopMate.DAL.Enums;
using ShopMate.DAL.Repository.Abstraction;

namespace ShopMate.BLL.Service.Implementation
{
    public class OrderServiceImp : IOrderService
    {
        private readonly IOrderRepo _orderRepo;

        public OrderServiceImp(IOrderRepo orderRepo)
        {
            _orderRepo = orderRepo;
        }

        public async Task<List<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepo.GetAllOrdersAsync();
            return orders.Select(o => o.ToOrderDto()).ToList();
        }

        public async Task<OrderDetailsDto> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepo.GetOrderByIdAsync(id);
            return order?.ToOrderDetailsDto();
        }

        public async Task<bool> UpdateOrderStatusAsync(int orderId, string newStatus)
        {
            var statusEnum = Enum.Parse<OrderStatus>(newStatus);
            await _orderRepo.UpdateOrderStatusAsync(orderId, statusEnum);
            return true;
        }
    }
}
