using ShopMate.BLL.DTO.AdminDto;
using ShopMate.BLL.Mapping;
using ShopMate.BLL.Service.Abstraction;
using ShopMate.DAL.Repository.Abstraction;

namespace ShopMate.BLL.Service.Implementation
{
    public class ContactMessageServiceImp : IContactMessageService
    {
        private readonly IContactMessageRepo _contactMessageRepo;

        public ContactMessageServiceImp(IContactMessageRepo contactMessageRepo)
        {
            _contactMessageRepo = contactMessageRepo;
        }

        public async Task<List<ContactMessageDto>> GetAllMessagesAsync()
        {
            var messages = await _contactMessageRepo.GetAllMessagesAsync();
            return messages.Select(m => m.ToContactMessageDto()).ToList();
        }

        public async Task<ContactMessageDto> GetMessageByIdAsync(int id)
        {
            var message = await _contactMessageRepo.GetMessageByIdAsync(id);
            return message?.ToContactMessageDto();
        }

        public async Task<bool> UpdateMessageStatusAsync(int messageId, string newStatus)
        {
            var statusEnum = Enum.Parse<ContactMessageStatus>(newStatus);
            await _contactMessageRepo.UpdateMessageStatusAsync(messageId, statusEnum);
            return true;
        }
    }
}
