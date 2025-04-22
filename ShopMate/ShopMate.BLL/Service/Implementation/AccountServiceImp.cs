using FluentValidation;
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
        private readonly IValidator<UpdateProfileDto> _updateProfileValidator;
        private readonly IValidator<ChangePasswordDto> _changePasswordValidator;
        private readonly IFileService _fileService;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;

        public AccountServiceImp(IAccountRepo accountRepo,
                                 IValidator<RegisterDto> registerValidator,
                                 IValidator<LoginDto> loginValidator,
                                 ITokenService tokenService,
                                 IEmailService emailService,
                                 IValidator<ResetPasswordDto> resetPasswordValidator,
                                 IFileService fileService,
                                 IValidator<UpdateProfileDto> updateProfileValidator,
                                 IValidator<ChangePasswordDto> changePasswordValidator)
        {
            _accountRepo = accountRepo;
            _registerValidator = registerValidator;
            _loginValidator = loginValidator;
            _tokenService = tokenService;
            _emailService = emailService;
            _resetPasswordValidator = resetPasswordValidator;
            _fileService = fileService;
            _updateProfileValidator = updateProfileValidator;
            _changePasswordValidator = changePasswordValidator;
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

        #region Login,register
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
            var userfromDb = await _accountRepo.GetUserByEmail(user.Email!);
            var res = await _accountRepo.CheckUserAsync(user, loginDto.Password!);

            if (res)
            {
                var claims = await _accountRepo.GetUserClaimsAsync(userfromDb);
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
                user.ProfileImagePath = await _fileService.SaveFileAsync(registerDto.ProfileImage);


                var res = await _accountRepo.CreateUserAsync(user, registerDto.Password!);
                if (res.Succeeded)
                {
                    //create token
                    var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.Id),
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim(ClaimTypes.Role, "Customer"),
                            new Claim(ClaimTypes.Name, user.UserName)
                        };
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


        #region Update Profile
        public async Task<Result> UpdateProfileAsync(UpdateProfileDto updateProfileDto)
        {
            var user = await _accountRepo.GetUserById(updateProfileDto.Id!);
            if (user == null)
            {
                return new Result
                {
                    Succeeded = false,
                    Errors = new List<string> { "User not found" }
                };
            }


            var validationResult = await _updateProfileValidator.ValidateAsync(updateProfileDto);

            if (validationResult.IsValid)
            {

                user.Email = updateProfileDto.Email;
                user.FirstName = updateProfileDto.FirstName;
                user.LastName = updateProfileDto.LastName;
                user.Gender = updateProfileDto.Gender;
                user.PhoneNumber = updateProfileDto.PhoneNumber;

                var profileImage = updateProfileDto.ProfileImage == null
                    ? user.ProfileImagePath
                    : await _fileService.SaveFileAsync(updateProfileDto.ProfileImage);
                user.ProfileImagePath = profileImage;

                var result = await _accountRepo.UpdateUserAsync(user);

                return new Result
                {
                    Succeeded = result.Succeeded,
                    Errors = result.Errors.Select(e => e.Description).ToList()
                };
            }
            return new Result
            {
                Succeeded = false,
                Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
            };

        }
        #endregion

        #region Delete Account,Logout
        public async Task<Result> DeleteAccount(string id)
        {
            var user = await _accountRepo.GetUserById(id);
            var result = await _accountRepo.DeleteAccount(user);

            if (result.Succeeded)
            {
                await DeleteUserImage(user.ProfileImagePath);
            }

            return new Result
            {
                Succeeded = result.Succeeded,
                Errors = result.Errors.Select(e => e.Description).ToList()
            };

        }
        private async Task DeleteUserImage(string profileImagePath)
        {
            if (profileImagePath != null)
            {
                _fileService.DeleteFileAsync(profileImagePath);
            }
        }

        public async Task<Result> ChangePasswordAsync(string userId, ChangePasswordDto changePasswordDto)
        {
            var validationResult = await _changePasswordValidator.ValidateAsync(changePasswordDto);

            if (!validationResult.IsValid)
            {
                return new Result
                {
                    Succeeded = false,
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var user = await _accountRepo.GetUserById(userId);
            if (user == null)
            {
                return new Result
                {
                    Succeeded = false,
                    Errors = new List<string> { "User not found." }
                };
            }

            var res = await _accountRepo.ChangePassword(user, changePasswordDto.CurrentPassword!, changePasswordDto.NewPassword!);

            return new Result
            {
                Succeeded = res.Succeeded,
                Errors = res.Errors.Select(e => e.Description).ToList()
            };
        }

        //public async Task LogoutAsync()
        //{
        //    await _accountRepo.LogoutAsync();
        //}
    }
}

#endregion









