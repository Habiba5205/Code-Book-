using CodeBook.Business.App.DTOs;
using CodeBook.Models.App;
using System;
namespace CodeBook.Business.App.Interfaces
{
    public interface IAuthService
    {
        bool Register(RegisterDto register);
        bool Login(LoginDto login);

    }
}