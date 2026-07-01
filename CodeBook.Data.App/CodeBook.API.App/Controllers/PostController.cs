using CodeBook.Business.App.DTOs;
using CodeBook.Business.App.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult GetPost(int id)
        {
            int? userId = null;
            if (User.Identity.IsAuthenticated)
            {
                userId = _currentUserInfo.GetCurrentUserId();
            }
            var post = _postService.GetPost(id, userId);
            if (post == null)
            {
                return NotFound(new { message = "Post not found or access denied" });
            }
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
            var userId= _currentUserInfo.GetCurrentUserId();
            var result = _postService.CreatePost(userId,request);
            if (result.Success)
            {
                return Ok(new { message = result.Message });
            }
            return BadRequest(new { message = result.Message });
        }

        [HttpPut("{id}/update")]
        [Authorize]
        public IActionResult UpdatePost(int id, [FromBody] UpdatePostRequest request)
        {
            if (request == null)
            {
                return BadRequest(new { message = "Invalid request" });
            }
            var userId = _currentUserInfo.GetCurrentUserId() ;
            var result = _postService.UpdatePost(id, request,userId);
            if (result.Success)
            {
                return Ok(new { message = result.Message });
            }
            return BadRequest(new { message = result.Message });
        }

        [HttpDelete("{id}/deletePost")]
        [Authorize]
        public IActionResult DeletePost(int id)
        {
            var userId = _currentUserInfo.GetCurrentUserId();
            var result = _postService.DeletePost(id, userId);
            if (result.Success)
            {
                return Ok(new { message = result.Message });
            }
            return BadRequest(new { message = result.Message });
        }

        [HttpPost("{id}/save")]
        [Authorize]
        public IActionResult SavePost(int id)
        {
            var userId = _currentUserInfo.GetCurrentUserId();
            var result = _postService.SavePost(userId, id);
            if (result.Success)
            {
                return Ok(new { message = result.Message });
            }
            return BadRequest(new { message = result.Message });
        }

        [HttpGet("{id}/tags")]
        [AllowAnonymous]
        public IActionResult GetPostTag(int id)
        {
            var tags = _postService.GetPostTags(id);
            if (tags == null || !tags.Any())
            {
                return NotFound(new { message = "No tags found for this post" });
            }
            return Ok(tags);
        }

        [HttpGet("search")]
        [AllowAnonymous]
        public IActionResult SearchPosts([FromQuery] string? key, [FromQuery] string? tag, [FromQuery] string? language)
        {
            var query = new SearchQuery
            {
                Keyword = key ?? string.Empty,
                Tag = tag ?? string.Empty,
                Language = language ?? string.Empty
            };

            var results = _searchService.SearchPosts(query);
            return Ok(results);
        }
    }
}
