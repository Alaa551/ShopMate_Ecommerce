namespace ShopMate.DAL.Database.Models
{
    public class WishListItem
    {
        public int Id { get; set; }

        public int WishListId { get; set; }
        public WishList WishList { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
