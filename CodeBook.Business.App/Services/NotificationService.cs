using CodeBook.Business.App.DTOs;
using CodeBook.Business.App.Interfaces;
using System;


namespace CodeBook.Business.App.Services
{
    public class NotificationService : INotificationService
    {

        public NotificationService() { }
        public void CreateNotification(int reciepentId, int? actorId, string type, int? postId, int? commentId, string message, string link)
        {

        }
        public List<NotificationDTO> GetUserNotification(int userId)
        {
            return new List<NotificationDTO>();
        }
        public void MarkAsRead(int notificationId)
        {

        }
    }
}
