using CodeBook.Business.App.DTOs;
using CodeBook.Models.App;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace CodeBook.Business.App.Interfaces
{
    public interface IAuthService
    {
        bool Register(RegisterDto register);
        string Login(LoginDto login);
        bool ResetPassword(ResetPasswordDto resetPassword);
        bool VerifyPassword(string password, int userId);

    }
}