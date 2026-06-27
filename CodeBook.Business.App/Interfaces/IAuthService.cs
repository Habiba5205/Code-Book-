using CodeBook.Models.App;
using System;
namespace CodeBook.Business.App.Interfaces
{
    public interface IAuthService
    {
        bool Register(string email, string password, string userName, string bio, string AvatarUrl, UserRole role);
        bool Login(string email, string password);

    }
}