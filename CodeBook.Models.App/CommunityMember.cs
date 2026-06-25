using CodeBook.Models.App.Enums;
namespace CodeBook.Models.App

{
	public class CommunityMember
	{
        public int UserId { set; get; }
        public int CommunityId { set; get; }
        public CommunityRole Role { set; get; }
		public DateTime JoinedAt { set; get; }

		public User User { set; get; }
		public CommunityMember Community { set; get; }
	}
}
