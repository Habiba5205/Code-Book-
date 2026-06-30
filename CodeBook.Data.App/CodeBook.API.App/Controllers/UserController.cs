using CodeBook.Business.App.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CodeBook.Business.App.DTOs;
using System.Security.Claims;
using CodeBook.Models.App;
using CodeBook.Business.App.Middleware;

namespace CodeBook.API.App.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class UserController : ControllerBase
    {

        private readonly IuserService _userService;
        private readonly CurrentUserInfo _currentUserInfo = new CurrentUserInfo();
        public UserController(IuserService userService) { _userService = userService; }


        [HttpGet("viewprofile")]
        [AllowAnonymous]
        public IActionResult GetProfile(int userId)
        {
            UserProfileResponse userProfile = _userService.GetProfile(userId);
            if (userProfile == null)
            {
                return NotFound(new { message = "User not found" });
            }
            return Ok(userProfile);
        }

        [HttpGet("viewmyprofile")]
        [Authorize]
        public IActionResult GetMyProfile()
        {
            int userId = _currentUserInfo.GetCurrentUserId();
            UserProfileResponse userProfile = _userService.GetProfile(userId);
            if (userProfile == null)
            {
                return NotFound(new { message = "User not found" });
            }
            return Ok(userProfile);
        }

        [HttpDelete("deletemyprofile")]
        [Authorize]
        public IActionResult DeleteProfile()
        {
            var currentid = _currentUserInfo.GetCurrentUserId();
            ErrorResponse result = _userService.DeleteAccount(currentid);
            if (result.Success)
            {
                return Ok(new {message = result.Message});
            }
            return BadRequest(new { message = result.Message });
        }

        [HttpPatch("updatemyprofile")]
        [Authorize]
        public IActionResult UpdateProfile(UpdateProfileDto updateProfile)
        {
            var currentid = _currentUserInfo.GetCurrentUserId();
            ErrorResponse result = _userService.UpdateProfile(currentid, updateProfile);
            if (result.Success)
            {
                return Ok(new { message = result.Message });

            }
            return BadRequest(new { message = result.Message });

        }

        [HttpPost("follow")]
        [Authorize]
        public IActionResult Follow(int userid)
        {
            var currentid = _currentUserInfo.GetCurrentUserId();
            if (currentid == userid)
            {
                return BadRequest(new { message = "You cannot follow yourself!" });
            }
            ErrorResponse result = _userService.Follow(currentid, userid);


            if (result.Success)
            {
                return Ok(new { message = result.Message});
            }
            return BadRequest(new { message = result.Message });
        }

        [HttpDelete("unfollow")]
        [Authorize]
        public IActionResult Unfollow(int userid)
        {
            var currentid = _currentUserInfo.GetCurrentUserId();
            if (currentid == userid)
            {
                return BadRequest(new { message = "You cannot unfollow yourself!" });
            }
            ErrorResponse result = _userService.Unfollow(currentid, userid);
            if (result.Success)
            {
                return Ok(new { message = result.Message });
            }
            return BadRequest(new { message = result.Message });

        }
    }
}
