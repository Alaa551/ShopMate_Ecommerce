using Microsoft.AspNetCore.Mvc;
using ShopMate.BLL.Service.Abstraction;
using ShopMate.BLL.Service.Abstraction;

namespace ShopMate.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public OrdersController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("place-order")]
        public async Task<IActionResult> PlaceOrder([FromQuery] string email, [FromQuery] string orderId)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(orderId))
                return BadRequest("Email and Order ID are required.");

            string subject = "🧾 Order Confirmation - ShopMate";
            string body = $@"
                <h2>Thank you for your order!</h2>
                <p>Your order <strong>#{orderId}</strong> has been received.</p>
                <p>We will notify you once it's shipped.</p>";

            await _emailService.SendEmailAsync(email, subject, body);
            return Ok("Order confirmation email sent.");
        }
    }
}
