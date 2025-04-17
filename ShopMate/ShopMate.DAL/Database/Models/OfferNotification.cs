namespace ShopMate.DAL.Database.Models
{
    public class OfferNotification : Notification
    {
        public int OfferId { get; set; }
        public Offer Offer { get; set; }
    }


}
