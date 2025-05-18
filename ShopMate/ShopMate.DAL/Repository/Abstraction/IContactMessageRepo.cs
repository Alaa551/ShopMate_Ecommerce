using ShopMate.DAL.Database.Models;
using ShopMate.DAL.Enums;

namespace ShopMate.DAL.Repository.Abstraction
{
    public interface IContactMessageRepo
    {
        
        Task<IEnumerable<ContactMessage>> GetAllMessagesAsync();

        Task<ContactMessage?> GetMessageByIdAsync(int id);

        Task UpdateMessageStatusAsync(int id, ContactMessageStatus newStatus);
    }
}
