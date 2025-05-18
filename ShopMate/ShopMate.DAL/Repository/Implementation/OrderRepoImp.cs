using Microsoft.EntityFrameworkCore;
using ShopMate.DAL.Database;
using ShopMate.DAL.Database.Models;
using ShopMate.DAL.Enums;
using ShopMate.DAL.Repository.Abstraction;

namespace ShopMate.DAL.Repository.Implementation
{
    public class OrderRepoImp : IOrderRepo
    {
        private readonly AppDbContext _context;

        public OrderRepoImp(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.ShippingAddress)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.ShippingAddress)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        // 3. Update order status
        public async Task UpdateOrderStatusAsync(int id, OrderStatus newStatus)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                order.OrderStatus = newStatus;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Order>> GetOrdersByStatusAsync(OrderStatus status)
        {
            return await _context.Orders
                .Where(o => o.OrderStatus == status)
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .ToListAsync();
        }
    }
}
