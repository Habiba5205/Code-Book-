using AutoMapper;
using BCrypt.Net;
using CodeBook.Business.App.DTOs;
using CodeBook.Business.App.Interfaces;
using CodeBook.Data.App.IRepositories;
using CodeBook.Models.App;
using CodeBook.Business.App.Mapping;
using System;

namespace CodeBook.Business.App.Services
{
    public class UserService : IuserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IFollowRepository _followRepository;
        private readonly IMapper mapper;
        public UserService(IUserRepository userRepository, IFollowRepository followRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _followRepository = followRepository;
            this.mapper = mapper;
        }
        public bool DeleteAccount(int userId) 
        {
            User user = _userRepository.GetProfileById(userId);
            
            _userRepository.Remove(user);
           return _userRepository.SaveChanges();

        }
        public UserProfileResponse GetProfile(int userId)
        {
            User user = _userRepository.GetProfileById(userId);
            if (user == null) return null;
            return mapper.Map<UserProfileResponse>(user);     
        }
        public bool UpdateProfile(int userId,UpdateProfileDto data) 
        {
            User user = _userRepository.GetProfileById(userId);

            user.Bio = data.Bio;
            user.UserName = data.UserName;
            user.AvatarUrl = data.AvatarUrl;
            _userRepository.Update(user);
            return _userRepository.SaveChanges();

        }
        public bool VerifyPassword(string password,int userId)
        {
            User user = _userRepository.GetProfileById(userId);

            return BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        }
        public bool Follow(int followerId, int followeeId)
        {
            Follow followedalready = _followRepository.GetFollow(followerId, followeeId);
            if (followedalready != null) return false;
            Follow follow = new Follow();
            follow.FollowerUserId = followerId;
            follow.FolloweeUserId = followeeId;
            _followRepository.AddFollow(follow);
           return _followRepository.SaveChanges();

        }

        public bool Unfollow(int followerId, int followeeId)
        {
            Follow follow = _followRepository.GetFollow(followerId, followeeId);
            if(follow == null) return false;
            _followRepository.RemoveFollow(follow);
            return _followRepository.SaveChanges();

        }
    }
}
