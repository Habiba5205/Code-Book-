using System;
using CodeBook.Business.App.DTOs;
using CodeBook.Business.App.Middleware;
using CodeBook.Models.App;
namespace CodeBook.Business.App.Interfaces
{
    public interface IReactionService
    {
        ErrorResponse AddPostReaction(int userId,ReactionDto reavtionDto);
        ErrorResponse AddCommentReaction(int userId, ReactionDto reavtionDto, int commentId);

        ErrorResponse RemovePostReaction(int postId, int userId);
        ErrorResponse RemoveCommentReaction(int postId, int userId,int commentId);

        ErrorResponse UpdateReaction(ReactionDto reactionDto, Reaction reaction);

    }
}
