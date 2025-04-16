using FluentValidation;
using Microsoft.Extensions.Configuration;
using ShopMate.BLL.DTO.AccountDto;
using ShopMate.BLL.Mapping;
using ShopMate.BLL.Service.Abstraction;
using ShopMate.DAL.Repository.Abstraction;
using System.Security.Claims;

namespace ShopMate.BLL.Service.Implementation
{
    public class AccountServiceImp : IAccountService
    {
        private readonly IAccountRepo _accountRepo;
        private readonly IValidator<RegisterDto> _registerValidator;
        private readonly IValidator<LoginDto> _loginValidator;
        private readonly IValidator<ResetPasswordDto> _resetPasswordValidator;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public AccountServiceImp(IAccountRepo accountRepo,
                                 IValidator<RegisterDto> registerValidator,
                                 IValidator<LoginDto> loginValidator,
                                 ITokenService tokenService,
                                 IEmailService emailService,
                                 IConfiguration configuration,
                                 IValidator<ResetPasswordDto> resetPasswordValidator)
        {
            _accountRepo = accountRepo;
            _registerValidator = registerValidator;
            _loginValidator = loginValidator;
            _tokenService = tokenService;
            _emailService = emailService;
            _configuration = configuration;
            _resetPasswordValidator = resetPasswordValidator;
        }

        #region Confirm Email
        public async Task<bool> SendConfirmEmailCode(string email)
        {
            var token = await _accountRepo.GetEmailConfirmationTokenAsync(email);
            if (token != null)
            {
                await _emailService.SendEmailConfirmationAsync(email, token);
                return true;
            }
            return false;
        }
        public async Task<bool> ConfirmEmail(string email, string token)
        {
            return await _accountRepo.ConfirmEmailAsync(email, token);
        }
        #endregion

        #region Login,register,google-login
        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _accountRepo.GetUserByEmail(email) != null;
        }

        public async Task<Result> Login(LoginDto loginDto)
        {
            var validationResult = await _loginValidator.ValidateAsync(loginDto);

            if (!validationResult.IsValid)
            {
                return new Result
                {
                    Succeeded = false,
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }
            var user = loginDto.ToApplicationUser();
            var res = await _accountRepo.CheckUserAsync(user, loginDto.Password!);

            if (res)
            {
                var claims = await _accountRepo.GetUserClaimsAsync(user);
                var token = _tokenService.GenerateToken(claims);
                return new Result
                {
                    Succeeded = true,
                    Token = token
                };


            }
            return new Result
            {
                Succeeded = false,
                Errors = new List<string> { "Invalid email or password" }
            };
        }


        public async Task<Result> Register(RegisterDto registerDto)
        {


            var validationResult = await _registerValidator.ValidateAsync(registerDto);

            if (validationResult.IsValid)
            {
                var user = registerDto.ToApplicationUser();

                var res = await _accountRepo.CreateUserAsync(user, registerDto.Password!);
                if (res.Succeeded)
                {
                    //create token
                    List<Claim> claims = new List<Claim>();

                    claims.Add(new Claim("Role", "Customer"));
                    claims.Add(new Claim("UserName", registerDto.UserName!));

                    await _accountRepo.AddClaimsAsync(user, claims);

                    return new Result
                    {
                        Succeeded = true
                    };

                }
            }
            return new Result
            {
                Succeeded = false,
                Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
            };


        }



        #endregion

        #region Forget Password
        public async Task<bool> SendResetPasswordToken(string email)
        {
            var token = await _accountRepo.GetResetPasswordTokenAsync(email);
            if (token != null)
            {
                await _emailService.SendResetPasswordTokenAsync(email, token);
                return true;
            }
            return false;
        }

        public async Task<Result> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var res = _resetPasswordValidator.ValidateAsync(resetPasswordDto);
            if (!res.Result.IsValid)
            {
                return new Result
                {
                    Succeeded = false,
                    Errors = res.Result.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var result = await _accountRepo.ResetPasswordAsync(resetPasswordDto.Email!,
                                                         resetPasswordDto.Token!,
                                                         resetPasswordDto.NewPassword!);

            return new Result
            {
                Succeeded = result.Succeeded,
                Errors = result.Errors.Select(e => e.Description).ToList()
            };
        }


        #endregion
    }
}





