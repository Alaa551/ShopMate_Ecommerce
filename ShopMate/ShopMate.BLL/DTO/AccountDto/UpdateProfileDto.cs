using Microsoft.AspNetCore.Http;
using ShopMate.DAL.Enums;

namespace ShopMate.BLL.DTO.AccountDto
{
    public class UpdateProfileDto
    {
        public string? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public IFormFile? ProfileImage { get; set; }
        public Gender Gender { get; set; }


    }
}
