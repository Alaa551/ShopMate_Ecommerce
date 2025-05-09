using Microsoft.AspNetCore.Mvc;
using ShopMate.BLL.Service.Abstraction;
using ShopMate.BLL.Service.Implementation;
using ShopMate.DAL.Repository.Abstraction;
using ShopMate.DAL.Repository.Implementation;

namespace ShopMate.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string keyword)
        {
            var results = await _productService.SearchProductsAsync(keyword);
            return Ok(results);
        }
    }
}
