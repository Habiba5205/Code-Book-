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
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
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
        [HttpGet]
       // [Authorize]
        public ActionResult GetNotification()
        {
            var notification = _notificationService.GetUserNotification(GetCurrentUserById());
            return Ok(notification);
        }

        [HttpPatch("{id}/read")]
       // [Authorize]
        public ActionResult MarkAsRead(int id)
        {
            _notificationService.MarkAsRead(id);
            return Ok(new { message = "Marked As Read!!" });
        }
        [HttpGet("unread-count")]
       // [Authorize]
        public ActionResult GetUnreadCount()
        {
            var count = _notificationService.GetUnreadNotificationCount(GetCurrentUserById());
            return Ok(new {unreadCount = count});
        }

    }
}
