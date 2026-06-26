using System;
namespace CodeBook.Business.App.Interfaces
{
    public interface IAuthService
    {
        bool Register(string email, string password, string userName);
        bool Login(string email, string password);

    }
}