using System;
using CodeBook.Business.App.Services;
using CodeBook.Data.App.IRepositories;
using CodeBook.Models.App;
using CodeBook.Business.App.Interfaces;
using BCrypt.Net;
using CodeBook.Business.App.DTOs;

namespace CodeBook.Business.App.Methods

{
	public class AuthService : IAuthService
	{
		private readonly IUserRepository _userRepository;


        public AuthService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}
		public bool Login(LoginDto login)
		{
            User existinguser = _userRepository.GetProfileByEmail(login.Email);
			if (existinguser == null)
				return false;

            return BCrypt.Net.BCrypt.Verify(login.Password,existinguser.PasswordHash);


        }
        public bool Register(RegisterDto register)
		{
			User existinguser = _userRepository.GetProfileByEmail(register.Email);
			if (existinguser != null)
				return false;
            
            User user = new User();
            user.Email = register.Email;
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(register.Password);
			user.UserName = register.UserName;
			user.Role = UserRole.NormalUser;

			_userRepository.Add(user);
			return _userRepository.SaveChanges();
        }
	
	}
}