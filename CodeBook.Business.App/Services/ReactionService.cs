using System;
using CodeBook.Business.App.Interfaces;
using CodeBook.Data.App;
using CodeBook.Models.App;
using CodeBook.Data.App.IRepositories;


namespace CodeBook.Business.App.Services
{
    public class ReactionService : IReactionService
    {
        private readonly IReactionRepository _reactionRepository;

        public ReactionService(IReactionRepository reactionRepository) 
        {
            this._reactionRepository = reactionRepository;
        }
        public void AddReaction(int userId,int postId,ReactionType reactionType)
        {
            Reaction reaction = new Reaction();
            reaction.UserId = userId;
            reaction.PostId = postId;
            reaction.Type = reactionType;
            _reactionRepository.Add(reaction);
            _reactionRepository.SaveChanges();
        }
        public void RemoveReaction(int postId,int userId)
        {
            Reaction reaction = _reactionRepository.GetReaction(postId,userId);
            _reactionRepository.Remove(reaction);
            _reactionRepository.SaveChanges();

        }
    }
}