using System;
using CodeBook.Models.App;
using CodeBook.Business.App.DTOs;
namespace CodeBook.Business.App.Interfaces
{
    public interface IuserService
    {
        bool DeleteAccount(int userId);
        UserProfileResponse GetProfile(int userId);
        bool UpdateProfile(int userId, UpdateProfileDto data);
        bool VerifyPassword(string password, int userId);
        bool Follow(int followerId, int followeeId);
        bool Unfollow(int followerId, int followeeId);

    }
}
