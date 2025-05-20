using ShopMate.DAL.Enums;

namespace ShopMate.BLL.DTO.AccountDto
{
    public class ProfileDto
    {
        public string? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ProfileImagePath { get; set; }
        public string? Address { get; set; }
        public Gender Gender { get; set; }


    }
}
