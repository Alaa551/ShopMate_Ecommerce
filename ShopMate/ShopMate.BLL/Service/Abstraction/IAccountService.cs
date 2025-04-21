using ShopMate.BLL.DTO.AccountDto;

namespace ShopMate.BLL.Service.Abstraction
{
    public interface IAccountService
    {
        Task<Result> Register(RegisterDto registerDto);
        Task<Result> Login(LoginDto loginDto);

        Task<Result> UpdateProfileAsync(UpdateProfileDto updateProfileDto);

        Task<Result> DeleteAccount(string id);

        Task<bool> EmailExistsAsync(string email);

        Task<bool> SendConfirmEmailCode(string email);
        Task<bool> ConfirmEmail(string email, string token);
        Task<bool> SendResetPasswordToken(string email);
        Task<Result> ResetPassword(ResetPasswordDto resetPasswordDto);

        //Task LogoutAsync();




    }
}
