namespace ShopMate.DAL.Database.Models
{
    public class WishList
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser User { get; set; }

        public ICollection<WishListItem> WishListItems { get; set; } = new List<WishListItem>();





    }
}
