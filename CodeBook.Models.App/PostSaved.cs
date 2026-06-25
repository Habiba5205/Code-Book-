namespace CodeBook.Models.App
{

	public class PostSaved
	{
		public int UserId { set; get; }
		public int PostId { set; get; }
		public DateTime SavedAt { set; get; } = DateTime.UtcNow;
		public User User { set; get; }
		public Post Post { set; get; }
		
	}
}