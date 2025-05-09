using Microsoft.AspNetCore.Mvc;
using ShopMate.BLL.Service.Abstraction;
using ShopMate.BLL.Service.Abstraction;

namespace ShopMate.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            var result = await _categoryService.SearchCategoriesAsync(keyword);
            return Ok(result);
        }
    }
}
