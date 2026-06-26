using System;
namespace CodeBook.Business.App.Services
{
    public interface IReactionService
    {
        void AddReaction(int userId, int postId, ReactionType reactionType);
        void RemoveReaction(int postId, int userId);

    }
}
