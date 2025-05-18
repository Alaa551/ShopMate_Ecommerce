using ShopMate.BLL.DTO.AdminDto;

namespace ShopMate.BLL.Service.Abstraction
{
    public interface IContactMessageService
    {
        Task<List<ContactMessageDto>> GetAllMessagesAsync();
        Task<ContactMessageDto> GetMessageByIdAsync(int id);
        Task<bool> UpdateMessageStatusAsync(int messageId, String newStatus);
    }
}
