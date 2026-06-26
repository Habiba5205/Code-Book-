namespace CodeBook.Models.App
{
	public class CommunityPosts
	{
		public int CommunityId { get; set; }
		public int PostId { get; set; }
 
		public ICollection<Post> Posts { get; set; } = new List<Post>();
		
	}
}