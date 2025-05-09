using Microsoft.AspNetCore.Mvc;
using ShopMate.BLL.Service.Abstraction; 

namespace ShopMate.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OffersController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public OffersController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("send-offer")]
        public async Task<IActionResult> SendOffer([FromQuery] string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return BadRequest("Email is required.");

            string subject = " Exclusive Offer Just for You!";
            string body = @"
                <h2 style='color:green;'>Special Offer!</h2>
                <p>We're excited to offer you 20% off on all products.</p>
                <p>Use code <strong>OFFER20</strong> at checkout.</p>
                <p>Hurry! Offer ends soon.</p>";

            await _emailService.SendEmailAsync(email, subject, body);
            return Ok("Offer email sent successfully.");
        }
    }
}
