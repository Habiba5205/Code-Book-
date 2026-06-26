namespace CodeBook.Models.App
{
    public class PostComments
    {
        public int PostId { get; set; }

        public Post Post { get; set; } = null!;
        public ICollection<Comment> Reactions { get; set; } = new List<Comment>();
    }
}