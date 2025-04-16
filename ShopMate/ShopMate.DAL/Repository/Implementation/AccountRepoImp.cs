using Microsoft.AspNetCore.Identity;
using ShopMate.DAL.Database.Models;
using ShopMate.DAL.Repository.Abstraction;
using System.Net;
using System.Security.Claims;

namespace ShopMate.DAL.Repository.Implementation
{
    public class AccountRepoImp : IAccountRepo
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountRepoImp(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
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
            var res = await _userManager.ResetPasswordAsync(user, WebUtility.UrlDecode(token), newPassword);
            return res;
        }

        public async Task<bool> IsUserNameExistsAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName) != null;
        }

        public async Task UpdateUserAsync(ApplicationUser applicationUser)
        {
            await _userManager.UpdateAsync(applicationUser);
        }
    }
}
