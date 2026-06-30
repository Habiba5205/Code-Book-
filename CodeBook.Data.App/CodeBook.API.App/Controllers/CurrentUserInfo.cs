using CodeBook.Models.App;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CodeBook.API.App.Controllers
{
    public class CurrentUserInfo : ControllerBase
    {
        public int GetCurrentUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (int.TryParse(userId, out int currentid))
            {
                return currentid;
            }
            throw new UnauthorizedAccessException();
        }
    }
}
