using CodeBook.Business.App.DTOs;
using CodeBook.Business.App.Interfaces;
using CodeBook.Business.App.Methods;
using CodeBook.Business.App.Services;
using CodeBook.Business.App.Validator;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CodeBook.API.App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
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
        public ActionResult SubmitReport([FromBody] ReportRequest request)
        {
            var response = _reportService.SubmitReport(GetCurrentUserById(), request);
            if(response != null && response.Success)
                return Ok(new { message = "Report Submitted Successfully!" });

            return BadRequest(new { message = "Couldn't Submit Report!" });

        }
    }
}
