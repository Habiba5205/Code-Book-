using CodeBook.Business.App.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CodeBook.Business.App.DTOs;
using System.Security.Claims;
using CodeBook.Models.App;

namespace CodeBook.API.App.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class UserController : ControllerBase
    {

        private readonly IuserService _userService;
        public UserController(IuserService userService) { _userService = userService; }

        private int GetCurrentUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (int.TryParse(userId, out int currentid))
            {
                return currentid;
            }
            throw new UnauthorizedAccessException();
        }

        [HttpGet("{userId}/viewprofile")]
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

        [HttpDelete("{userId}/deleteprofile")]
        [Authorize]
        public IActionResult DeleteProfile(int userId)
        {
            var currentid = GetCurrentUserId();
            if (userId != currentid)
            {
                return Forbid();
            }
            if (_userService.DeleteAccount(userId))
            {
                return NoContent();
            }
            return BadRequest(new { message = "Couldn't be deleted!" });
        }

        [HttpPatch("{userid}/updateprofile")]
        [Authorize]
        public IActionResult UpdateProfile(UpdateProfileDto updateProfile)
        {
            var currentid = GetCurrentUserId();
            if (_userService.UpdateProfile(currentid, updateProfile))
            {
                return Ok(new { message = "Profile Updated!" });

            }
            return BadRequest(new { message = "Couldn't Update!" });

        }

        [HttpPost("follow")]
        [Authorize]
        public IActionResult Follow(int userid)
        {
            var currentid = GetCurrentUserId();
            if (currentid == userid)
            {
                return BadRequest(new { message = "You cannot follow yourself!" });
            }
            
            if (_userService.Follow(currentid, userid))
            {
                return Ok(new { message = "Followed!" });
            }
            return BadRequest(new { message = "Couldn't follow" });
        }

        [HttpDelete("unfollow")]
        [Authorize]
        public IActionResult Unfollow(int userid)
        {
            var currentid = GetCurrentUserId();
            if (currentid == userid)
            {
                return BadRequest(new { message = "You cannot unfollow yourself!" });
            }
            if (_userService.Unfollow(currentid, userid))
            {
                return Ok(new { message = "Unfollowed!" });
            }
            return BadRequest(new { message = "Couldn't follow" });

        }
    }
}
