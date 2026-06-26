namespace CodeBook.Models.App
{
	public class RemovalReports
	{
		public int RemovalId { get; set; }
		public ICollection<Report> Reports { get; set; } = new List<Report>();
	}
}