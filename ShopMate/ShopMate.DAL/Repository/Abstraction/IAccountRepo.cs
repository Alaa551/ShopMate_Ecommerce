using Microsoft.AspNetCore.Identity;
using ShopMate.DAL.Database.Models;
using System.Security.Claims;

namespace ShopMate.DAL.Repository.Abstraction
{
    public interface IAccountRepo
    {
        Task<IdentityResult> CreateUserAsync(ApplicationUser applicationUser, string password);
        Task<IdentityResult> UpdateUserAsync(ApplicationUser applicationUser);
        Task<bool> CheckUserAsync(ApplicationUser applicationUser, string password);
        Task AddClaimsAsync(ApplicationUser applicationUser, IList<Claim> claims);
        Task<ApplicationUser> GetUserByEmail(string email);
        Task<bool> IsUserNameExistsAsync(string userName);
        Task<IList<Claim>> GetUserClaimsAsync(ApplicationUser user);
        Task<string> GetEmailConfirmationTokenAsync(string email);
        Task<bool> ConfirmEmailAsync(string email, string token);

        Task<string> GetResetPasswordTokenAsync(string email);
        Task<IdentityResult> ResetPasswordAsync(string email, string token, string newPassword);
        Task<ApplicationUser> GetUserById(string id);

        Task<IdentityResult> DeleteAccount(ApplicationUser user);

        //Task LogoutAsync();
    }
}
