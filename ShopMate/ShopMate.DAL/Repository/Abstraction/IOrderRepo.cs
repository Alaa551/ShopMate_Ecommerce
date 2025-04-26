using ShopMate.DAL.Database.Models;
using ShopMate.DAL.Enums;

namespace ShopMate.DAL.Repository.Abstraction
{
    public interface IOrderRepo
    {
        
        Task<IEnumerable<Order>> GetAllOrdersAsync();

        Task<Order?> GetOrderByIdAsync(int id);

        Task UpdateOrderStatusAsync(int id, OrderStatus newStatus);

        Task<IEnumerable<Order>> GetOrdersByStatusAsync(OrderStatus status);
    }
}
