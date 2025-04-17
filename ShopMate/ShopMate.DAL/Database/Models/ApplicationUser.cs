using Microsoft.AspNetCore.Identity;
using ShopMate.DAL.Enums;


namespace ShopMate.DAL.Database.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? Address { get; set; }
        public Gender Gender { get; set; } = Gender.Male;

        public WishList? WishList { get; set; }

        public Cart? Cart { get; set; }
        public ICollection<ShippingAddress> ShippingAddresses { get; set; } = new List<ShippingAddress>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();

        public ICollection<ProductReview> ProductReviews { get; set; } = new List<ProductReview>();

        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();

        public ICollection<ContactMessage> ContactMessages { get; set; } = new List<ContactMessage>();

    }
}