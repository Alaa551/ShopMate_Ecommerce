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


        public static ProfileDto ToProfile(this ApplicationUser applicationUser) =>

       new ProfileDto
       {
           Id = applicationUser.Id,
           FirstName = applicationUser.FirstName,
           LastName = applicationUser.LastName,
           UserName = applicationUser.UserName,
           Email = applicationUser.Email,
           PhoneNumber = applicationUser.PhoneNumber,
           Address = applicationUser.Address,
           Gender = applicationUser.Gender,
           ProfileImagePath = applicationUser.ProfileImagePath
       };

    }
}
