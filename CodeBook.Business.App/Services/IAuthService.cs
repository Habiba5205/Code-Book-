using System;
namespace CodeBook.Business.App.Services

public interface IAuthService
{
    bool isActive(string userId);
}
