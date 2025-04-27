using Microsoft.AspNetCore.Mvc;
using ShopMate.BLL.DTO.AdminDto;
using ShopMate.BLL.Service.Abstraction;

namespace ShopMate.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IProductReviewService _reviewService;

        public ReviewsController(IProductReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        // GET: api/reviews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductReviewDto>>> GetAllReviews()
        {
            var reviews = await _reviewService.GetAllReviewsAsync();
            return Ok(reviews);
        }

        // GET: api/reviews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductReviewDto>> GetReviewById(int id)
        {
            var review = await _reviewService.GetReviewByIdAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            return Ok(review);
        }

        // GET: api/products/5/reviews
        [HttpGet("products/{productId}/reviews")]
        public async Task<ActionResult<IEnumerable<ProductReviewDto>>> GetReviewsByProductId(int productId)
        {
            var reviews = await _reviewService.GetReviewsByProductIdAsync(productId);
            return Ok(reviews);
        }
    }
}