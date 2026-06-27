using System;
using CodeBook.Business.App.Services;
using CodeBook.Data.App.IRepositories;
using CodeBook.Models.App;
using CodeBook.Business.App.Interfaces;
using BCrypt.Net;

namespace CodeBook.Business.App.Methods

{
	public class AuthService : IAuthService
	{
		private readonly IUserRepository _userRepository;


        public AuthService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}
		public bool Login(string email, string password)
		{
            User existinguser = _userRepository.GetProfileByEmail(email);
            if (existinguser != null)
                throw new Exception("Email Not Found!!");

            return BCrypt.Net.BCrypt.Verify(password,existinguser.PasswordHash);


        }
        public bool Register(string email, string password,string userName, string bio, string AvatarUrl, UserRole role)
		{
			User existinguser = _userRepository.GetProfileByEmail(email);
			if (existinguser != null)
				throw new Exception("Email Already Exists!!Just Login");
            
            User user = new User();
            user.Email = email;
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
			user.UserName = userName;
			user.Bio = bio;
			user.Role = UserRole.NormalUser;
			user.AvatarUrl = AvatarUrl;

			_userRepository.Add(user);
			_userRepository.SaveChanges();
			return true;
        }
	
	}
}