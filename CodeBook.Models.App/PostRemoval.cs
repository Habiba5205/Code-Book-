namespace CodeBook.Models.App
{

	public class PostRemoval:BaseEntity

	{
		public int PostId { set; get; }
		public int RemoverId { set; get; }
		public int? ReportId { set; get; }
		public string Reason { set; get; }

		public Post Post { set; get; }
		public User Remover { set; get; }
		public Report? Report { set; get; }
	
	}
}