using Microsoft.EntityFrameworkCore;
using ShopMate.DAL.Database;
using ShopMate.DAL.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMate.DAL.Repository.Implementation
{
    public class Product_Repo
    {
        private readonly AppDbContext _context;
        public async Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm)
        {
            return await _context.Products
                .Where(p => p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm))
                .ToListAsync();
        }
    }
}
