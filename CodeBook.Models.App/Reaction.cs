using CodeBook.Models.App.Enums;
namespace CodeBook.Models.App
{

    public class Reaction : BaseEntity
    {
        public int UserId { set; get; }
        public int? PostId { set; get; }
        public int? CommentId { set; get; }
        public ReactionType Type { set; get; }

        public User User { set; get; }
        public Post? Post { set; get; }
        public Comment? Comment { set; get; }

    }
}