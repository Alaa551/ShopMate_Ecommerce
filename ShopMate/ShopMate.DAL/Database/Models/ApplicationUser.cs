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
    }
}
