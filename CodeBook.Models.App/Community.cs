namespace CodeBook.Models.App

{
	public class Community : BaseEntity
	{
		public int OwnerId { set; get; }
		public string Name { set; get; }
		public string? Description { set; get; }
		public string? IconURL { set; get; }
		public string Slug { set; get; }

		public User Owner { set; get; }
		public Icollection<Post> Posts { set; get; } = new List<Post>();
		public ICollection<CommunityMember> Members { set; get; } = new List<CommunityMember>();

	}
}