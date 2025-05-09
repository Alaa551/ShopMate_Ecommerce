using ShopMate.BLL.Service.Abstraction;
using ShopMate.DAL.Database.Models;
using ShopMate.DAL.Repository.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMate.BLL.Service.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryService _categoryRepository;

        public CategoryService(ICategoryService categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> SearchCategoriesAsync(string keyword)
        {
            return await _categoryRepository.SearchCategoriesAsync(keyword);
        }
    }

}
