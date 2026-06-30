using System;
using CodeBook.Business.App.DTOs;
using CodeBook.Business.App.Middleware;
using CodeBook.Models.App;
namespace CodeBook.Business.App.Interfaces
{
    public interface IReactionService
    {
        ErrorResponse AddReaction(int userId,ReactionDto reavtionDto);
        ErrorResponse RemoveReaction(int postId, int userId);
        ErrorResponse UpdateReaction(ReactionDto reactionDto, Reaction reaction);

    }
}
