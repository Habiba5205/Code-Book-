using System;
namespace CodeBook.Business.App.Services
{
    public interface IuserService
    {
        void DeleteAccount(string userId) { }
        void GetProfile(string userId) { }
        void updateProfile(string userId) { }
        bool VerifyPassword(string password) { }



    }
}
