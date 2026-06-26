using System;
namespace CodeBook.Business.App.Services
{
    public interface IEmailService
    {
        Task SendVerificationEmail(string email, string cerificationLink);
        Task SendPasswordResetEmail(string email, string resetLink);
    }
}
