using CodeBook.Business.App.DTOs;
using CodeBook.Business.App.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.AccessControl;


namespace CodeBook.API.App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly ISearchService _searchService;
        private readonly ICommentService _commentService;

        public PostController(IPostService postService, ISearchService searchService, ICommentService commentService)
        {
            _postService = postService;
            _searchService = searchService;
            _commentService = commentService;
        }

        [HttpGet("feed")]
        [AllowAnonymous]
        public IActionResult GetFeed([FromQuery] int page = 1)
        {
            var feed = _postService.GetFeed(page);
            return Ok(feed);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult GetPost(int id)
        {
            var post = _postService.GetPost(id);
            if (post == null)
            {
                return NotFound(new { message = "Post not found" });
            }
            return Ok(post);
        }

        [HttpPost("create")]
        //[Authorize]
        public IActionResult CreatePost([FromBody] CreatePostRequest request)
        {
            if (request == null)
            {    
                return BadRequest(new { message = "Invalid request" });
            }
            request.TagIds ??= new List<int>();
            _postService.CreatePost(request);
            return Ok(new { message = "Post created successfully" });
        }

        [HttpPut("{id}/update")]
        //[Authorize]
        public IActionResult UpdatePost(int id, [FromBody] UpdatePostRequest request)
        {
            if (request == null)
            {
                return BadRequest(new { message = "Invalid request" });
            }
            _postService.UpdatePost(id, request);
            return Ok(new { message = "Post updated successfully" });
        }

        [HttpDelete("{id}/delete")]
        //[Authorize]
        public IActionResult DeletePost(int id)
        {
            _postService.DeletePost(id);
            return Ok(new { message = "Post deleted successfully" });
        }

        [HttpPost("{id}/save")]
       // [Authorize]
        public IActionResult SavePost(int id, [FromQuery] int userId)
        {
            _postService.SavePost(userId, id);
            return Ok(new { message = "Post saved successfully" });
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

        [HttpGet("/search")]
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

        [HttpGet("{postId}/comments")]
        [AllowAnonymous]
        public IActionResult GetComments(int postId)
        {
            var comments = _commentService.GetPostComments(postId);
            return Ok(comments);
        }

        [HttpPost("{postId}/comments")]
       // [Authorize]
        public IActionResult AddComment(int id, [FromBody] AddCommentRequest request)
        {
            if(request == null)
            {
                return BadRequest(new {message = "Invalid request" });
            }
            _commentService.AddComment(request.AuthorId, id, request.Body, request.SelfCommentId);
            return Ok(new { message = "Comment added successfully" });
        }
    }
}
