using ShopMate.DAL.Database.Models;
using ShopMate.DAL.Database;
using ShopMate.DAL.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ShopMate.DAL.Repository.Implementation
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;

        public CartRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CartItem>> SearchAsync(int id)
        {
            return await _context.CartItems
                .Where(c => c.Id==id)
                .ToListAsync();
        }
    }
}
