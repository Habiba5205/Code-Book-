namespace CodeBook.Models.App
{
	public class Post: BaseEntity
	{
		public string Title { set; get; }
		public string Body { set; get; }
		public string? CodeSnippet { set; get; }
		public string? Language { set; get; }
		public int LikeCount { set; get; }
		public int CommentCount { set; get; }
		public bool IsRemoved { set; get; }

		public int AuthorId { set; get; }
		public int CommunityId { set; get; }

		public User Author { set; get; }
		public Community? Community { set; get; }
		public ICollection<Comment> Comments { set; get; } = new List<Comment>();
		public ICollection<Reaction> Reactions { set; get; } = new List<Reaction>();
		public Icollection<PostTag> PostTags { set; get; } = new List<PostTag>();
		public Icollection<PostSaved> SavedByUsers { set; get; } = new List<PostSaved>();
		public Icollection<Report> Reports { set; get; } = new List<Report>();
		public ICollection<PostRemoval> Removals { set; get; } = new List<PostRemoval>(); 
    }
}