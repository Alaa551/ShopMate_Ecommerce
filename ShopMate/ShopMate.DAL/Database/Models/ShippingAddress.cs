namespace ShopMate.DAL.Database.Models
{
    public class ShippingAddress
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser User { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }



}
