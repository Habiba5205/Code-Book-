namespace CodeBook.Models.App

{
	public class Follow
	{
		public int FollowerUserId { set; get; }
		public int FollowedUserId { set; get; }
		public DateTime CreatedAt { set; get; } = DateTime.UtcNow;

		public User Follower { set; get; }
		public User Followed { set; get; }
	

}
}