using System.ComponentModel.DataAnnotations.Schema;

namespace ShopMate.DAL.Database.Models
{
    public class Offer
    {
        public int Id { get; set; }

        public string? Description { get; set; }

        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public DateTime EndDate { get; set; } = DateTime.UtcNow.AddDays(7);
        public decimal DiscountPercentage { get; set; }

        [NotMapped]
        public bool IsActive => DateTime.UtcNow >= StartDate && DateTime.UtcNow <= EndDate;

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public ICollection<OfferNotification> OfferNotifications { get; set; } = new List<OfferNotification>();
    }


}
