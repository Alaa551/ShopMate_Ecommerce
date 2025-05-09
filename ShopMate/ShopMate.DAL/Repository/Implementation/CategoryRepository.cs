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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> SearchAsync(string keyword)
        {
            return await _context.Categories
                .Where(c => c.Name.Contains(keyword))
                .ToListAsync();
        }
    }
}
