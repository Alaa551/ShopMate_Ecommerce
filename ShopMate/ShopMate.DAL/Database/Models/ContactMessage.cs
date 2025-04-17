namespace ShopMate.DAL.Database.Models
{
    public class ContactMessage
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ContactMessageStatus Status { get; set; } = ContactMessageStatus.Open;

        public string ApplicationUserId { get; set; }
        public ApplicationUser User { get; set; }
    }


}
