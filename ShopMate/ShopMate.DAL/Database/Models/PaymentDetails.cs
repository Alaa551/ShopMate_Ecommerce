namespace ShopMate.DAL.Database.Models
{
    public class PaymentDetails
    {
        public int Id { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

        public decimal TotalAmount { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }


}
