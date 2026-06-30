using CodeBook.Business.App.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CodeBook.Business.App.Services;
using CodeBook.Business.App.Interfaces;

namespace CodeBook.API.App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly CurrentUserInfo _currentUserInfo;

        public CommentController(ICommentService commentService, CurrentUserInfo currentUserInfo)
        {
            _commentService = commentService;
            _currentUserInfo = currentUserInfo;
        }

        [HttpGet("{postId}/comments")]
        [AllowAnonymous]
        public IActionResult GetComments(int postId)
        {
            var comments = _commentService.GetPostComments(postId);
            return Ok(comments);
        }

        [HttpPost("{id}/comments")]
        [Authorize]
        public IActionResult AddComment(int id, [FromBody] AddCommentRequest request)
        {
            if (request == null)
            {
                return BadRequest(new { message = "Invalid request" });
            }
            var userId = _currentUserInfo.GetCurrentUserId();
            _commentService.AddComment(userId, id, request);
            return Ok(new { message = "Comment added successfully" });
        }

        [HttpDelete("{id}/deleteComment")]
        [Authorize]
        public IActionResult DeleteComment(int id)
        {
            _commentService.DeleteComment(id);
            return Ok(new { message = "Comment deleted successfully" });
        }
    }
}
