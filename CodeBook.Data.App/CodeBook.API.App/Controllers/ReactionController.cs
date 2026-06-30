using CodeBook.Business.App.DTOs;
using CodeBook.Business.App.Interfaces;
using CodeBook.Business.App.Methods;
using CodeBook.Business.App.Services;
using CodeBook.Business.App.Validator;
using CodeBook.Models.App;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CodeBook.API.App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReactionController : ControllerBase
    {
        private readonly IReactionService _reactionService;

        public ReactionController(IReactionService reactionService) 
        { 
            _reactionService = reactionService; 
        }

        private int GetCurrentUserById()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (int.TryParse(userId, out int currentid))
            {
                return currentid;
            }
            throw new UnauthorizedAccessException();
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddReaction([FromBody] ReactionDto reactionDto)
        {
            var currentId = GetCurrentUserById();
            if(_reactionService.AddReaction(currentId,reactionDto).Success)
                return Ok(new { message = "Reaction Added Successfully!" });

            return BadRequest(new { message = "Couldn't Add Reaction!" });
        }

        [HttpDelete("{postId}")]
        [Authorize]
        public ActionResult RemoveReaction(int postId)
        {
            var currentId = GetCurrentUserById();
            if (_reactionService.RemoveReaction(postId,currentId).Success)
                return Ok(new { message = "Reaction Removed!" });

            return BadRequest(new { message = "Couldn't Remove Reaction!" });
        }
    }
}
