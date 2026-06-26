namespace CodeBook.Models.App
{
	public class PostReactions
	{
		public int PostId { get; set; }

		public Post Post { get; set; } = null!;
		public ICollection<Reaction> Reactions { get; set; } = new List<Reaction>();
	}
}