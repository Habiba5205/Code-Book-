using CodeBook.Business.App.DTOs;
using CodeBook.Business.App.Interfaces;
using CodeBook.Data.App;
using CodeBook.Models.App;
using System;


namespace CodeBook.Business.App.Services
{
    public class NotificationService : INotificationService
    {
        private readonly CodeBookContext context;
        public NotificationService(CodeBookContext context)
        {
            this.context = context;
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
            return context.notifications
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
