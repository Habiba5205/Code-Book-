using System;
using CodeBook.Business.App.DTOs;
namespace CodeBook.Business.App.Services
{
    public interface INotificationService
    {
        Task CreateNotificationAsync(int reciepentId, int? actorId, string type, int? postId, int? commentId, string message, string link);
        Task <List<NotificationDTO>> GetUserNotificationAsync(int userId);
        Task MarkAsRead(int notificationId);
    }
}
