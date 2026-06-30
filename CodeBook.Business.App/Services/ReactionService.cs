using System;
using CodeBook.Business.App.Interfaces;
using CodeBook.Data.App;
using CodeBook.Models.App;
using CodeBook.Data.App.IRepositories;
using CodeBook.Business.App.DTOs;
using CodeBook.Business.App.Middleware;


namespace CodeBook.Business.App.Services
{
    public class ReactionService : IReactionService
    {
        private readonly IReactionRepository _reactionRepository;
        private readonly INotificationService _notificationService;
        private readonly IPostService _postService;

        public ReactionService(IReactionRepository reactionRepository, INotificationService notificationService, IPostService postService) 
        {
            this._reactionRepository = reactionRepository;
            _notificationService = notificationService;
            _postService = postService;
        }
        public ErrorResponse AddReaction(int userId,ReactionDto reactionDto)
        {
            Reaction reaction = new Reaction();
            reaction.UserId = userId;
            reaction.PostId = reactionDto.PostId;
            reaction.Type = Enum.Parse<ReactionType>(reactionDto.ReactionType);
            _reactionRepository.Add(reaction);
            bool result = _reactionRepository.SaveChanges();

            _notificationService.CreateNotification(_postService.GetPostAuthorId(reactionDto.PostId), new NotificationDTO
            {
                UserId = _postService.GetPostAuthorId(reactionDto.PostId),
                Type = "Reaction",
                Message = "Someone reacted to your post",
                ReferenceId = reactionDto.PostId,
                IsSeen = false,
                DateCreated = DateTime.UtcNow
            });
            if( result) return new ErrorResponse { Success = true, Message = "Reacted" };
            else return new ErrorResponse { Success = false, Message = "Failed to react" };
        }
        public ErrorResponse RemoveReaction(int postId,int userId)
        {
            Reaction reaction = _reactionRepository.GetReaction(postId,userId);
            _reactionRepository.Remove(reaction);
           bool result = _reactionRepository.SaveChanges();
            if(!result) return new ErrorResponse { Success = false, Message = "Failed to remove reaction" };
            else return new ErrorResponse { Success = true, Message = "Reaction removed" };

        }
    }
}