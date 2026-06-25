using CodeBook.Models.App.Enums;
namespace CodeBook.Models.App
{
    public class Report : BaseEntity
    {
        public int ReporterId { set; get; }
        public int? PostId { set; get; }
        public int? CommentId { set; get; }
        public string reason { set; get; }
        public ReportStatus Status { set; get; }

        public User Reporter { set; get; }
        public Post? Post { set; get; }

    }
}
