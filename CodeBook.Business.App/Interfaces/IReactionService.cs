using System;
using CodeBook.Models.App;
namespace CodeBook.Business.App.Interfaces
{
    public interface IReactionService
    {
        void AddReaction(int userId, int postId, ReactionType reactionType);
        void RemoveReaction(int postId, int userId);

    }
}
