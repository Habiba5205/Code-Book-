using System;
using CodeBook.Business.App.DTOs;
namespace CodeBook.Business.App.Interfaces
{
    public interface INotificationService
    {
        void CreateNotification(int reciepentId, int? actorId, string type, int? postId, int? commentId, string message, string link);
        List<NotificationDTO> GetUserNotification(int userId);
        void MarkAsRead(int notificationId);
    }
}
