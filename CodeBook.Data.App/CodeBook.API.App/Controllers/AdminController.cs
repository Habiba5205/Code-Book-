using CodeBook.Business.App.DTOs;
using CodeBook.Business.App.Interfaces;
using CodeBook.Business.App.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Claims;

namespace CodeBook.API.App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Policy ="AdminOnly")]
    public class AdminController : ControllerBase
    {
        private readonly IModerationService _moderationService;
        private readonly IReportService _reportService;


        private int GetCurrentUserById() { 
          var userId = User.FindFirstValue(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (int.TryParse(userId, out int currentid))
            {
                return currentid;
            }
            throw new UnauthorizedAccessException();
        }
        public AdminController(IModerationService moderationService, IReportService reportService)
        {
            _moderationService = moderationService;
            _reportService = reportService;
        }

        [HttpDelete("posts/{id}")]
        public IActionResult RemovePost(int id, [FromBody] RemovePostsDto dto)
        {
            try
            {
                var removerId = GetCurrentUserById();
                if (string.IsNullOrEmpty(dto.Reason))
                    return BadRequest("Reason is required");
                _moderationService.RemovePost(id, dto,removerId);
                return Ok("Post removed successfully");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex) {               
                return BadRequest(ex.Message);
             }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("reports")]
        public IActionResult GetReports()
        {
            try
            {
                var reports = _reportService.GetPendingReports();
                if (!reports.Any())
                    return NotFound("No pending reports found.");
                return Ok(reports);
            }
            catch (Exception ex) { 
            return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPatch("reports/{id}/status")]
        public IActionResult UpdateReportStatus(int id, [FromBody] UpdateReportStatusDto dto)
        {
            try { 
             _reportService.UpdateReportStatus(id, dto);
                return Ok("Report Status Updated Successfully");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }



        }
    }
}
