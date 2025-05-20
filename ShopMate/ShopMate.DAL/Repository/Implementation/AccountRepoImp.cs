using Microsoft.AspNetCore.Identity;
using ShopMate.DAL.Database.Models;
using ShopMate.DAL.Repository.Abstraction;
using System.Security.Claims;

namespace ShopMate.DAL.Repository.Implementation
{
    public class AccountRepoImp : IAccountRepo
    {
        private readonly UserManager<ApplicationUser> _userManager;
        // private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountRepoImp(UserManager<ApplicationUser> userManager
            //  SignInManager<ApplicationUser> signInManager
            )
        {
            _userManager = userManager;
            //  _signInManager = signInManager;
        }

        public async Task AddClaimsAsync(ApplicationUser applicationUser, IList<Claim> claims) =>

           await _userManager.AddClaimsAsync(applicationUser, claims);


        public async Task<bool> CheckUserAsync(ApplicationUser applicationUser, string password)
        {
            var user = await _userManager.FindByEmailAsync(applicationUser.Email!);
            if (user == null)
            {
                return false;
            }
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<bool> ConfirmEmailAsync(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return false;
            var res = await _userManager.ConfirmEmailAsync(user, token);
            return res.Succeeded;

        }

        public async Task<IdentityResult> CreateUserAsync(ApplicationUser applicationUser, string password)
        {
            var res = await _userManager.CreateAsync(applicationUser, password);
            return res;
        }

        public async Task<ApplicationUser> GetUserByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<string> GetEmailConfirmationTokenAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return null;

            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<string> GetResetPasswordTokenAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return null;

            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<IList<Claim>> GetUserClaimsAsync(ApplicationUser user)
        {

            return await _userManager.GetClaimsAsync(user);
        }

        public async Task<IdentityResult> ResetPasswordAsync(string email, string token, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return null;
            var res = await _userManager.ResetPasswordAsync(user, token, newPassword);
            return res;
        }

        public async Task<bool> IsUserNameExistsAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName) != null;
        }

        public async Task<IdentityResult> UpdateUserAsync(ApplicationUser applicationUser)
        {
            return await _userManager.UpdateAsync(applicationUser);
        }

        public async Task<ApplicationUser> GetUserById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<IdentityResult> DeleteAccount(ApplicationUser user)
        {
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "User not found" });
            return await _userManager.DeleteAsync(user);
        }

        public async Task<IdentityResult> ChangePassword(ApplicationUser user, string currentPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        }

        //public async Task LogoutAsync()
        //{
        //    await _signInManager.SignOutAsync();
        //}
    }
}
