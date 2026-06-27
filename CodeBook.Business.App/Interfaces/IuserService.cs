using System;
using CodeBook.Models.App;
using CodeBook.Business.App.DTOs;
namespace CodeBook.Business.App.Interfaces
{
    public interface IuserService
    {
        void DeleteAccount(int userId);
        UserProfileResponse GetProfile(int userId);
        void UpdateProfile(int userId, UpdateProfileDto data);
        bool VerifyPassword(string password, int userId);
        void Follow(int followerId, int followeeId);
        void Unfollow(int followerId, int followeeId);

    }
}
