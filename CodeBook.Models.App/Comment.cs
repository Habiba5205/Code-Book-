namespace CodeBook.Models.App
{
	public class Comment : BaseEntity
	{
		public string Body { set; get; }
		public int LikeCount { set; get; }
		public int AuthorId { set; get; }
		public int PostId { set; get; }
		public int? SelfCommentId { set; get; } //for reply

		public User Author { set; get; }
		public Post Post { set; get; }
		public Comment? selfComment { set; get; }
		public ICollection<Comment> Replies { set; get; } = new List<Comment>();
		public ICollection<Reaction> Reactions { set; get; } = new List<Reaction>();
	}
}
