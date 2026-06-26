using CodeBook.Models.App.Enums;
namespace CodeBook.Models.App
{
    public class Report : BaseEntity
    {
        public int ReporterId { get; set; }
        public int? PostId { get; set; }
        public int? CommentId { get; set; }
        public string Reason { get; set; } = string.Empty;
        public string? Description { get; set; }
        public ReportStatus Status { get; set; }

        public User Reporter { get; set; } = null!;
        public Post? Post { get; set; }

        }
}
