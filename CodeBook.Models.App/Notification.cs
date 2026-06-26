
using CodeBook.Models.App.Enums;
namespace CodeBook.Models.App
{
    public class Notification : BaseEntity
    {
        public NotificationType Type {get; set;}
        public int UserId {get; set;}
        public string Message {get; set;}
        public int ReferenceId {get; set;}
        public bool IsSeen {get; set;}

        public User User { get; set; } = null!;

    }

}