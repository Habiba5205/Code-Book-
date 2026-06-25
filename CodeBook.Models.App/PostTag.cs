namespace CodeBook.Models.App
{


	public class PostTag
	{
		public int PostId { set; get; }
		public int TagId { set; get; }
		public Post Post { set; get; }
		public Tag Tag { set; get; }
	}
}