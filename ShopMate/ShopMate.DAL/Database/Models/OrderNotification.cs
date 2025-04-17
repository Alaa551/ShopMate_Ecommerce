namespace ShopMate.DAL.Database.Models
{
    public class OrderNotification : Notification
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }   
    
    
}
