using Microsoft.AspNetCore.Mvc;
using ShopMate.BLL.Service.Abstraction;

namespace ShopMate.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] int keyword)
        {
            var result = await _cartService.SearchCartAsync(keyword);
            return Ok(result);
        }
    }
}
