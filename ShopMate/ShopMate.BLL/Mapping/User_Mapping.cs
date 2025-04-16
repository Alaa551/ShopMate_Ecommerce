using ShopMate.BLL.DTO.AccountDto;
using ShopMate.DAL.Database.Models;

namespace ShopMate.BLL.Mapping
{
    public static class User_Mapping
    {

        public static ApplicationUser ToApplicationUser(this RegisterDto registerDto) =>

            new ApplicationUser
            {
                UserName = registerDto.UserName,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                Gender = registerDto.Gender,
                Address = registerDto.Address,

            };

        public static ApplicationUser ToApplicationUser(this LoginDto loginDto) =>

         new ApplicationUser
         {
             Email = loginDto.Email,
         };

    }
}
