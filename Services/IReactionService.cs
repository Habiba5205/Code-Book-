namespace CodeBook.Services
{
    public interface IReactionService
    {
        void AddReaction(string userId,string postId,string reaction) { }
        void RemoveReaction(string postId) { }

    }
}
