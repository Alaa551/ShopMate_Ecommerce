using Microsoft.EntityFrameworkCore;
using ShopMate.DAL.Database;
using ShopMate.DAL.Database.Models;
using ShopMate.DAL.Enums;
using ShopMate.DAL.Repository.Abstraction;

namespace ShopMate.DAL.Repository.Implementation
{
    public class ContactMessageRepoImp : IContactMessageRepo
    {
        private readonly AppDbContext _context;

        public ContactMessageRepoImp(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ContactMessage>> GetAllMessagesAsync()
        {
            return await _context.ContactMessages
                .Include(cm => cm.User)
                .OrderByDescending(cm => cm.CreatedAt)
                .ToListAsync();
        }

        public async Task<ContactMessage?> GetMessageByIdAsync(int id)
        {
            return await _context.ContactMessages
                .Include(cm => cm.User)
                .FirstOrDefaultAsync(cm => cm.Id == id);
        }

        public async Task UpdateMessageStatusAsync(int id, ContactMessageStatus newStatus)
        {
            var message = await _context.ContactMessages.FindAsync(id);
            if (message != null)
            {
                message.Status = newStatus;
                await _context.SaveChangesAsync();
            }
        }
    }
}
