using CodeBook.Business.App.DTOs;
using CodeBook.Business.App.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.AccessControl;
using System.Security.Claims;


namespace CodeBook.API.App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly ISearchService _searchService;
        private readonly ICommentService _commentService;
        private readonly CurrentUserInfo _currentUserInfo;

        public PostController(IPostService postService, ISearchService searchService, ICommentService commentService, CurrentUserInfo currentUserInfo)
        {
            _postService = postService;
            _searchService = searchService;
            _commentService = commentService;
            _currentUserInfo = currentUserInfo;
        }
        [HttpGet("feed")]
        [AllowAnonymous]
        public IActionResult GetFeed([FromQuery] int page = 1)
        {
            int? userId = null;
            if (User.Identity.IsAuthenticated)
            { 
                userId = _currentUserInfo.GetCurrentUserId();
            }
            var feed = _postService.GetFeed(page, userId);

            return Ok(feed);
        }

        [HttpGet("{postId}")]
        [AllowAnonymous]
        public IActionResult GetPost(int postId)
        {
            int? userId = null;
            if (User.Identity.IsAuthenticated)
            {
                userId = _currentUserInfo.GetCurrentUserId();
            }
            var post = _postService.GetPost(postId, userId);
            if (post == null)
            {
                return NotFound(new { message = "Post not found or access denied" });
            }
            if (userId != null && post.AuthorId == userId) { post.isOwner = true; }
            return Ok(post);
        }

        [HttpPost("create")]
        [Authorize]
        public IActionResult CreatePost([FromBody] CreatePostRequest request)
        {
            if (request == null)
            {    
                return BadRequest(new { message = "Invalid request" });
            }
            request.TagIds ??= new List<int>();
            var userId = _currentUserInfo.GetCurrentUserId();

            var result = request.CommunityId == 0
                ? _postService.CreatePost(userId, request, null)                    
                : _postService.CreatePost(userId, request, request.CommunityId);

            if (result.Success)
            {
                return Ok(new { message = result.Message });
            }
            return BadRequest(new { message = result.Message });
        }

        [HttpPut("{postId}/update")]
        [Authorize]
        public IActionResult UpdatePost(int postId, [FromBody] UpdatePostRequest request)
        {
            if (request == null)
            {
                return BadRequest(new { message = "Invalid request" });
            }
            var userId = _currentUserInfo.GetCurrentUserId() ;
            var result = _postService.UpdatePost(postId, request, userId);
            if (result.Success)
            {
                return Ok(new { message = result.Message });
            }
            return BadRequest(new { message = result.Message });
        }

        [HttpDelete("{postId}/deletePost")]
        [Authorize]
        public IActionResult DeletePost(int postId)
        {
            var userId = _currentUserInfo.GetCurrentUserId();
            try
            {
                var result = _postService.DeletePost(postId, userId);

                return Ok(new { message = result.Message });
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.InnerException.Message });

            }

        }

        [HttpPost("{postId}/save")]
        [Authorize]
        public IActionResult SavePost(int postId)
        {
            var userId = _currentUserInfo.GetCurrentUserId();
            var result = _postService.SavePost(userId, postId);
            if (result.Success)
            {
                return Ok(new { message = result.Message });
            }
            return BadRequest(new { message = result.Message });
        }

        [HttpGet("saved")]
        [Authorize]
        public IActionResult GetSavedPosts()
        {
            var userId = _currentUserInfo.GetCurrentUserId();
            var posts = _postService.GetSavedPosts(userId);
            return Ok(posts);
        }

        [HttpDelete("{id}/unsave")]
        [Authorize]
        public IActionResult UnsavePost(int id)
        {
            var userId = _currentUserInfo.GetCurrentUserId();
            var result = _postService.UnsavePost(userId, id);

            if (result.Success)
                return Ok(new { message = result.Message });

            return BadRequest(new { message = result.Message });
        }

        [HttpGet("{postId}/tags")]
        [AllowAnonymous]
        public IActionResult GetPostTag(int postId)
        {
            var tags = _postService.GetPostTags(postId);
            if (tags == null || !tags.Any())
            {
                return NotFound(new { message = "No tags found for this post" });
            }
            return Ok(tags);
        }

        [HttpGet("search")]
        [AllowAnonymous]
        public IActionResult SearchPosts([FromQuery] string? keyword,
                                  [FromQuery] string? language,
                                  [FromQuery] string? tag)
        {
            var results = _postService.SearchPosts(keyword, language, tag);
            return Ok(results);
        }

        [HttpGet("myposts")]
        [Authorize]
        public IActionResult GetMyPosts()
        {
            var userId = _currentUserInfo.GetCurrentUserId();
            var posts = _postService.GetUserPosts(userId);
            return Ok(posts);
        }
    }
}
