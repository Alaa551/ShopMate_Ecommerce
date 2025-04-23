using BLL.Settings;
using Microsoft.Extensions.Options;
using ShopMate.BLL.Service.Abstraction;
using System.Net;
using System.Net.Mail;

namespace ShopMate.BLL.Service.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly SmtpSettings _smtpSettings;
        public EmailService(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            using (var client = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port))
            {
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password);

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_smtpSettings.FromEmail!, _smtpSettings?.FromName),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true
                };
                mailMessage.To.Add(email);

                await client.SendMailAsync(mailMessage);
            }
        }

        public async Task SendEmailConfirmationAsync(string email, string token)
        {
            //var confirmationUrl = $"{_smtpSettings.ClientAppUrl}/ConfirmEmail?email={WebUtility.UrlEncode(email)}&token={WebUtility.UrlEncode(token)}";
            //var message = $"Please confirm your email by clicking <a href='{confirmationUrl}'>here</a>";
            var message = $"Your confirmation code is: <strong>{token}</strong><br/>It will expire in 30 minutes.";
            await SendEmailAsync(email, "Confirm your email", message);
        }

        public async Task SendResetPasswordTokenAsync(string email, string token)
        {
            //here link should be to reset password frontend page 
            var confirmationUrl = $"{_smtpSettings.ClientAppUrl}/ResetPassword?email={WebUtility.UrlEncode(email)}&token={WebUtility.UrlEncode(token)}";
            var message = $"Please reset your password by clicking <a href='{confirmationUrl}'>here</a>";
            await SendEmailAsync(email, "Reset your Password", message);
        }
    }

}


