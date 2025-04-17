﻿namespace ShopMate.DAL.Database.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();




    }
}
