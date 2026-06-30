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
        private readonly CurrentUserInfo _currentUserInfo = new CurrentUserInfo();
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("getnotification")]
        [Authorize]
        public ActionResult GetNotification()
        {
            var notification = _notificationService.GetUserNotification(_currentUserInfo.GetCurrentUserId());
            return Ok(notification);
        }

        [HttpPatch("readNotification")]
        [Authorize]
        public ActionResult MarkAsRead(int id)
        {
            _notificationService.MarkAsRead(id);
            return Ok(new { message = "Marked As Read!!" });
        }

        [HttpGet("GetUnreadCount")]
        [Authorize]
        public ActionResult GetUnreadCount()
        {
            var count = _notificationService.GetUnreadNotificationCount(_currentUserInfo.GetCurrentUserId());
            return Ok(new {unreadCount = count});
        }

    }
}
