namespace ShopMate.DAL.Database.Models
{
    public class ProductReview
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
        public DateTime ReviewDate { get; set; } = DateTime.UtcNow;


        public string? ApplicationUserId { get; set; }
        public ApplicationUser? User { get; set; }

        public string? Comment { get; set; }
        public int Rating { get; set; }
    }
}
