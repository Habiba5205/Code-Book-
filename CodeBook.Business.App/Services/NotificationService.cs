using CodeBook.Business.App.DTOs;
using CodeBook.Business.App.Interfaces;
using AutoMapper;
using CodeBook.Data.App;
using CodeBook.Models.App;
using System;


namespace CodeBook.Business.App.Services
{
    public class NotificationService : INotificationService
    {
        private readonly CodeBookContext context;
        private readonly IMapper mapper;
        public NotificationService(CodeBookContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public void CreateNotification(int userId, NotificationType type, int referenceId, string message)
        {
            var notification = new Notification
            {
                UserId = userId,
                Type = type,
                ReferenceId = referenceId,
                Message = message,
                IsSeen = false,
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow
            };
            context.notifications.Add(notification);
            context.SaveChanges();
        }
        public List<NotificationDTO> GetUserNotification(int userId)
        {
            var notifications = context.notifications
                .Where(n => n.UserId == userId)
                .ToList();
            return mapper.Map<List<NotificationDTO>>(notifications);

          /*  return context.notifications
                .Where(n => n.UserId == userId)
                .Select(n => new NotificationDTO
                {
                    Id = n.Id,
                    Type = n.Type.ToString(),
                    Message = n.Message,
                    ReferenceId = n.ReferenceId,
                    IsSeen = n.IsSeen,
                    DateCreated = n.DateCreated
                }).ToList();
          */
        }
        public void MarkAsRead(int notificationId)
        {
            var notification = context.notifications.FirstOrDefault(n => n.Id == notificationId);
            
            if (notification != null)
            {
                notification.IsSeen = true;
                notification.DateUpdated = DateTime.UtcNow;
                context.SaveChanges();
            }
        }
    }
}
