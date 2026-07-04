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
        private readonly ICommentService _commentService;
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;



        public ReactionService(IReactionRepository reactionRepository, INotificationService notificationService, IPostService postService,ICommentService commentService, IPostRepository postRepository,ICommentRepository commentRepository) 
        {
            this._reactionRepository = reactionRepository;
            _notificationService = notificationService;
            _postService = postService;
            _commentService = commentService;
            _postRepository = postRepository;
            _commentRepository = commentRepository;
        }
        public ErrorResponse AddPostReaction(int userId,ReactionDto reactionDto)
        {
            Reaction reaction = _reactionRepository.GetReaction(reactionDto.PostId,userId);
            if(reaction != null )
            {
                return UpdateReaction(reactionDto, reaction);
            }

            reaction = new Reaction();
            reaction.UserId = userId;
            reaction.PostId = reactionDto.PostId;
            reaction.Type = Enum.Parse<ReactionType>(reactionDto.ReactionType);
            _reactionRepository.Add(reaction);
            _postRepository.AddReaction(reactionDto.PostId, reaction.Type,userId);

            bool result = _reactionRepository.SaveChanges();

            _notificationService.CreateNotification(_postService.GetPostAuthorId(reactionDto.PostId), new NotificationDTO
            {
                userId = _postService.GetPostAuthorId(reactionDto.PostId),
                Type = "Reaction",
                Message = "Someone reacted to your post",
                ReferenceId = reactionDto.PostId,
                IsSeen = false,
                DateCreated = DateTime.UtcNow
            });
            if(result) return new ErrorResponse { Success = true, Message = "Reacted" };
            else return new ErrorResponse { Success = false, Message = "Failed to react" };
        }
        public ErrorResponse RemovePostReaction(int postId,int userId)
        {
            Reaction reaction = _reactionRepository.GetReaction(postId,userId);
            if (reaction == null) return new ErrorResponse { Success = false, Message = "You didn't react to this post before!" };
            _reactionRepository.Remove(reaction);
            _postRepository.RemoveReaction(postId);
            bool result = _reactionRepository.SaveChanges();
            if(!result) return new ErrorResponse { Success = false, Message = "Failed to remove reaction" };
            else return new ErrorResponse { Success = true, Message = "Reaction removed" };

        }
        public ErrorResponse AddCommentReaction(int userId, ReactionDto reactionDto,int commentId)
        {
            Reaction reaction = _reactionRepository.GetCommentReaction(reactionDto.PostId, userId,commentId);
            if (reaction != null)
            {
                return UpdateReaction(reactionDto, reaction);
            }

            reaction = new Reaction();
            reaction.UserId = userId;
            reaction.PostId = reaction.PostId;
            reaction.CommentId = commentId;
            reaction.Type = Enum.Parse<ReactionType>(reactionDto.ReactionType);
            _reactionRepository.Add(reaction);
            _commentRepository.AddReaction(commentId);

            bool result = _reactionRepository.SaveChanges();

            _notificationService.CreateNotification(_commentService.GetCommentAuthorId(commentId), new NotificationDTO
            {
                userId = _commentService.GetCommentAuthorId(commentId),
                Type = "Reaction",
                Message = "Someone reacted to your Comment",
                ReferenceId = reactionDto.PostId,
                IsSeen = false,
                DateCreated = DateTime.UtcNow
            });
            if (result) return new ErrorResponse { Success = true, Message = "Reacted" };
            else return new ErrorResponse { Success = false, Message = "Failed to react" };
        }
        public ErrorResponse RemoveCommentReaction(int postId, int userId,int commentId)
        {
            Reaction reaction = _reactionRepository.GetCommentReaction(postId, userId,commentId);
            if (reaction == null) return new ErrorResponse { Success = false, Message = "You didn't react to this Comment before!" };
            _reactionRepository.Remove(reaction);
           _commentRepository.RemoveReaction(commentId);

            bool result = _reactionRepository.SaveChanges();
            if (!result) return new ErrorResponse { Success = false, Message = "Failed to remove reaction" };
            else return new ErrorResponse { Success = true, Message = "Reaction removed" };

        }
        public ErrorResponse UpdateReaction(ReactionDto reactionDto, Reaction reaction)
        {
            reaction.Type = Enum.Parse<ReactionType>(reactionDto.ReactionType);
            reaction.DateCreated = DateTime.UtcNow;
            _reactionRepository.Update(reaction);
            bool result = _reactionRepository.SaveChanges();
            if (result)
            {
                return new ErrorResponse { Success = true, Message = "Reaction Updated!" };
            }
            else return new ErrorResponse { Success = false, Message = "Couldn't Update" };
        }
    }
}