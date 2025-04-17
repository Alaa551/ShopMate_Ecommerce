using ShopMate.DAL.Enums;

namespace ShopMate.DAL.Database.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;


        public int ShippingAddressId { get; set; }
        public ShippingAddress ShippingAddress { get; set; }

        public string? ApplicationUserId { get; set; }
        public ApplicationUser? User { get; set; }

        public int? CouponId { get; set; }
        public Coupon? Coupon { get; set; }

        public int PaymentId { get; set; }
        public PaymentDetails PaymentDetails { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public ICollection<OrderNotification> OrderNotifications { get; set; } = new List<OrderNotification>();




    }



}
