using CodeBook.Models.App.Enums;
namespace CodeBook.Models.App
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public UserRole Role { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string? Bio { get; set; }
        public string? ProfilePicURL { get; set; }

        public ICollection<Post> Posts { get; set; } = new List<Post>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Reaction> Reactions { get; set; } = new List<Reaction>();
        public ICollection<Follow> Followers { get; set; } = new List<Follow>();
        public ICollection<Community> Communities { get; set; } = new List<Community>();
        public ICollection<CommunityMember> CommunityMembership { get; set; } = new List<CommunityMember>();
        public ICollection<PostSaved> SavedPosts { get; set; } = new List<PostSaved>();
        public ICollection<Follow> Following { get; set; } = new List<Follow>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}
