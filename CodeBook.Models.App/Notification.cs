
using CodeBook.Models.App.Enums;
namespace CodeBook.Models.App
{
    public class Notification : BaseEntity
    {
        public NotificationType Type { set; get; }
        public int UserId { set; get; }
        public string Message { set; get; }
        public int ReferenceId { set; get; }
        public bool IsSeen { set; get; }

        public User User { set; get; }

    }

}