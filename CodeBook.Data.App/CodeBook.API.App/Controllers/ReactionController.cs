using CodeBook.Business.App.DTOs;
using CodeBook.Business.App.Interfaces;
using CodeBook.Business.App.Methods;
using CodeBook.Business.App.Middleware;
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
        private readonly CurrentUserInfo _currentUserInfo;

        public ReactionController(IReactionService reactionService, CurrentUserInfo currentUserInfo)
        {
            _reactionService = reactionService;
            _currentUserInfo = currentUserInfo;
        }


        [HttpPost("addreaction")]
        [Authorize]
        public ActionResult AddReaction([FromBody] ReactionDto reactionDto)
        {
            var currentId = _currentUserInfo.GetCurrentUserId();
            ErrorResponse result = _reactionService.AddReaction(currentId, reactionDto);
            if (result.Success)
                return Ok(new { message = result.Message});

            return BadRequest(new { message = result.Message });
        }

        [HttpDelete("removereaction")]
        [Authorize]
        public ActionResult RemoveReaction(int postId)
        {
            var currentId = _currentUserInfo.GetCurrentUserId();
            ErrorResponse result = _reactionService.RemoveReaction(postId, currentId);
            if (result.Success)
                return Ok(new { message = result.Message });

            return BadRequest(new { message = result.Message });
        }
    }
}
