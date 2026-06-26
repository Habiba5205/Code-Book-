using System;
namespace CodeBook.Business.App.Services
{
    public interface IuserService
    {
        void DeleteAccount(int userId);
        User GetProfile(int userId);
        void updateProfile(int userId, UpdateProfileDto data);
        bool VerifyPassword(string password);
        void Follow(int followerId, int followeeId);
        void Unfollow(int followerId, int followeeId);

    }
}
