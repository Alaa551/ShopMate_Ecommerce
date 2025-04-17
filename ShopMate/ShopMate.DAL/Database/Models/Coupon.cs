using System.ComponentModel.DataAnnotations.Schema;

namespace ShopMate.DAL.Database.Models
{
    public class Coupon
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public decimal DiscountPercentage { get; set; }
        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public DateTime EndDate { get; set; } = DateTime.UtcNow.AddDays(7);

        [NotMapped]
        public bool IsActive => DateTime.UtcNow >= StartDate && DateTime.UtcNow <= EndDate;

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }


}
