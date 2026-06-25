namespace CodeBook.Models.App
{


	public class Tag:BaseEntity
	{
		public string Name { set; get; }
		public string Slug { set; get; }

		public ICollection<PostTag> PostTags { set; get; } = new List<PostTag>();
	}
}
