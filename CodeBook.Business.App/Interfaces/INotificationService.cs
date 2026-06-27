using CodeBook.Business.App.DTOs;
using CodeBook.Models.App;
using System;
namespace CodeBook.Business.App.Interfaces
{
    public interface INotificationService
    {
        public void CreateNotification(int userId, NotificationType type, int referenceId, string message);
        List<NotificationDTO> GetUserNotification(int userId);
        void MarkAsRead(int notificationId);
    }
}
