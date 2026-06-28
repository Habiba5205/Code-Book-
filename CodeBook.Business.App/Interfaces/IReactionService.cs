using System;
using CodeBook.Business.App.DTOs;
using CodeBook.Models.App;
namespace CodeBook.Business.App.Interfaces
{
    public interface IReactionService
    {
        bool AddReaction(int userId,ReactionDto reavtionDto);
        bool RemoveReaction(int postId, int userId);

    }
}
