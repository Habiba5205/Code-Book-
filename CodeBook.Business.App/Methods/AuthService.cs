using System;
using CodeBook.Business.App.Services;
namespace CodeBook.Business.App.Methods

{
	public class AuthService : IAuthService
	{
        private CodeBookContext Authdata;


        public AuthService(CodeBookContext Authdata)
		{
			this.Authdata = Authdata;
		}
		public bool Login(string email, string password)
		{
            User existinguser = Authdata.Users.FirstOrDefault(e => e.Email == email);
            if (existinguser == null)
                throw new Exception("Email Not Found!!");

            return BCrypt.Net.BCrypt.Verify(password,existinguser.PasswordHash);


        }
        public bool Register(string email, string password,string userName)
		{
			User existinguser = Authdata.Users.FirstOrDefault(e =>  e.Email == email);
			if (existinguser != null)
				throw new Exception("Email Already Exists!!Just Login");
            
            User user = new User();
            user.Email = email;
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
			user.UserName = userName;
			Authdata.Users.Add(user);
			Authdata.SaveChanges();
			return true;
        }
	
	}
}