using CodeBook.Business.App.DTOs;
using CodeBook.Business.App.Interfaces;
using AutoMapper;
using CodeBook.Data.App.IRepositories;
using CodeBook.Models.App;
using System;


namespace CodeBook.Business.App.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper mapper;
        public NotificationService(INotificationRepository notificationRepository, IMapper mapper)
        {
            this._notificationRepository = notificationRepository;
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
            _notificationRepository.Add(notification);
            _notificationRepository.SaveChanges();
        }
        public List<NotificationDTO> GetUserNotification(int userId)
        {
            var notifications = _notificationRepository.GetNotificationsbyUserId(userId);
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
            var notification = _notificationRepository.GetbyNotificationId(notificationId);
            
            if (notification != null)
            {
                notification.IsSeen = true;
                notification.DateUpdated = DateTime.UtcNow;
                _notificationRepository.Update(notification);
                _notificationRepository.SaveChanges();
            }
        }
    }
}
