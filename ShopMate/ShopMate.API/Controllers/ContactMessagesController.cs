using Microsoft.AspNetCore.Mvc;
using ShopMate.BLL.DTO.AdminDto;
using ShopMate.BLL.Service.Abstraction;

namespace ShopMate.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IContactMessageService _messageService;

        public MessagesController(IContactMessageService messageService)
        {
            _messageService = messageService;
        }

        // GET: api/messages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactMessageDto>>> GetAllMessages()
        {
            var messages = await _messageService.GetAllMessagesAsync();
            return Ok(messages);
        }

        // GET: api/messages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactMessageDto>> GetMessageById(int id)
        {
            var message = await _messageService.GetMessageByIdAsync(id);
            if (message == null)
            {
                return NotFound();
            }
            return Ok(message);
        }

        // PUT: api/messages/5/status
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateMessageStatus(int id, [FromBody] string newStatus)
        {
            try
            {
                var result = await _messageService.UpdateMessageStatusAsync(id, newStatus);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}