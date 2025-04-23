namespace ShopMate.BLL.Service.Abstraction
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
        Task SendEmailConfirmationAsync(string email, string token);
        Task SendResetPasswordTokenAsync(string email, string token);
    }
}