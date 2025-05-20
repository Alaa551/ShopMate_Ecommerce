using ShopMate.BLL.Service.Abstraction;
using ShopMate.DAL.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMate.BLL.Service.Implementation
{
    public class ProductService:IProductService
    {
        private readonly IProductService _productRepository;
        public async Task<IEnumerable<Product>> SearchProductsAsync(string searchTerm)
        {
            return await _productRepository.SearchProductsAsync(searchTerm);
        }
    }
}
