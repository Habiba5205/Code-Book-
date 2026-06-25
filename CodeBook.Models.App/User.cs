
namespace CodeBook.Models.App
{
    public class User : BaseEntity
    {
        public string Email { set; get; }
        public string UserName { set; get; }
        public string password { set; get; }
        public string? BIO { set; get; }
        public string? ProfilePicURL { set; get; }

        public ICollection<Post> Posts { set; get; } = new List<Post>();
        public ICollection<Comment> Comments { set; get; } = new List<Comment>();
        public ICollection<Reaction> Reactions { set; get; } = new List<Reaction>();
        public ICollection<Follow> Followers { set; get; } = new List<Follow>();
        public ICollection<Community> Communities { set; get; } = new List<Community>();
        public ICollection<CommunityMember> CommunityMembers { set; get; } = new List<CommunityMember>();
        public ICollection<PostSaved> SavedPosts { set; get; } = new List<PostSaved>();
        public ICollection<Follow> Following { set; get; } = new List<Follow>();
        public ICollection<Notification> Notifications { set; get; } = new List<Notification>();
    }
}
