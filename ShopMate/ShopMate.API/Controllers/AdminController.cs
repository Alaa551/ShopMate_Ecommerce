using Microsoft.AspNetCore.Mvc;
using ShopMate.BLL.DTO.AdminDto;
using ShopMate.BLL.Service.Abstraction;

namespace ShopMate.Web.Controllers
{
    [ApiController]
    [Route("admin")]
    public class AdminController : ControllerBase
    {
        private readonly IContactMessageService _messageService;
        private readonly IOrderService _orderService;
        private readonly IProductReviewService _reviewService;

        public AdminController(
            IContactMessageService messageService,
            IOrderService orderService,
            IProductReviewService reviewService)
        {
            _messageService = messageService;
            _orderService = orderService;
            _reviewService = reviewService;
        }

        // -----------------------
        //  Messages Endpoints
        // -----------------------

        [HttpGet("messages")]
        public async Task<ActionResult<IEnumerable<ContactMessageDto>>> GetAllMessages()
        {
            var messages = await _messageService.GetAllMessagesAsync();
            return Ok(messages);
        }

        [HttpGet("messages/{id}")]
        public async Task<ActionResult<ContactMessageDto>> GetMessageById(int id)
        {
            var message = await _messageService.GetMessageByIdAsync(id);
            if (message == null)
                return NotFound();
            return Ok(message);
        }

        [HttpPut("messages/{id}/status")]
        public async Task<IActionResult> UpdateMessageStatus(int id, [FromBody] string newStatus)
        {
            try
            {
                var result = await _messageService.UpdateMessageStatusAsync(id, newStatus);
                if (!result)
                    return NotFound();
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // -----------------------
        //  Orders Endpoints
        // -----------------------

        [HttpGet("orders")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("orders/{id}")]
        public async Task<ActionResult<OrderDetailsDto>> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound();
            return Ok(order);
        }

        [HttpPut("orders/{id}/status")]
        public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] string newStatus)
        {
            try
            {
                var result = await _orderService.UpdateOrderStatusAsync(id, newStatus);
                if (!result)
                    return NotFound();
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("orders/status/{status}")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersByStatus(string status)
        {
            try
            {
                var orders = await _orderService.GetAllOrdersAsync();
                var filteredOrders = orders.Where(o => o.OrderStatus.Equals(status, StringComparison.OrdinalIgnoreCase));
                return Ok(filteredOrders);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // -----------------------
        //  Reviews Endpoints
        // -----------------------

        [HttpGet("reviews")]
        public async Task<ActionResult<IEnumerable<ProductReviewDto>>> GetAllReviews()
        {
            var reviews = await _reviewService.GetAllReviewsAsync();
            return Ok(reviews);
        }

        [HttpGet("reviews/{id}")]
        public async Task<ActionResult<ProductReviewDto>> GetReviewById(int id)
        {
            var review = await _reviewService.GetReviewByIdAsync(id);
            if (review == null)
                return NotFound();
            return Ok(review);
        }

        [HttpGet("products/{productId}/reviews")]
        public async Task<ActionResult<IEnumerable<ProductReviewDto>>> GetReviewsByProductId(int productId)
        {
            var reviews = await _reviewService.GetReviewsByProductIdAsync(productId);
            return Ok(reviews);
        }
    }
}
