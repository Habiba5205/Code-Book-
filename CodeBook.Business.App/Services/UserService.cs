using System;
using CodeBook.Business.App.Interfaces;
using CodeBook.Data.App;
using CodeBook.Business.App.DTOs;
using CodeBook.Models.App;
using BCrypt.Net;

namespace CodeBook.Business.App.Services
{
    public class UserService : IuserService
    {
        private CodeBookContext userdata;
        public UserService(CodeBookContext userData)
        {
            userdata = userData;
        }
        public void DeleteAccount(int userId) 
        {
            User user = userdata.users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
                throw new Exception("User Not Found");
            
            userdata.users.Remove(user);
            userdata.SaveChanges();

        }
        public User GetProfile(int userId) 
        {
            User user = userdata.users.FirstOrDefault(u=>u.Id == userId);
            if (user == null)
                throw new Exception("User Not Found");

            return user;        
        }
        public void UpdateProfile(int userId,UpdateProfileDto data) 
        {
            User user = userdata.users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
                throw new Exception("User Not Found!Please Create an Account");

            user.Bio = data.Bio;
            user.UserName = data.UserName;
            user.AvatarUrl = data.AvatarUrl;
            userdata.SaveChanges();

        }
        public bool VerifyPassword(string password,int userId)
        {
            User user = userdata.users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
                throw new Exception("User Not Found!");

            return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        }
        public void Follow(int followerId, int followeeId)
        {
            Follow follow = new Follow();
            follow.FollowerUserId = followerId;
            follow.FolloweeUserId = followeeId;
            userdata.follows.Add(follow);
            userdata.SaveChanges();


        }

        public void Unfollow(int followerId, int followeeId)
        {
            Follow follow = userdata.follows.FirstOrDefault(f =>f.FollowerUserId == followerId && f.FolloweeUserId == followeeId);
            if (follow == null)
                throw new Exception("Follow Record Not Found");

            userdata.follows.Remove(follow);
            userdata.SaveChanges();

        }
    }
}
