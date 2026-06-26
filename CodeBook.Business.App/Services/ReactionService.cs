using System;
using CodeBook.Business.App.Interfaces;
using CodeBook.Data.App;
using CodeBook.Models.App;


namespace CodeBook.Business.App.Services
{
    public class ReactionService : IReactionService
    {
        private CodeBookContext Reactiondata;

        public ReactionService(CodeBookContext reactionData ) 
        {
            Reactiondata = reactionData;
        }
        public void AddReaction(int userId,int postId,ReactionType reactionType)
        {
            Reaction reaction = new Reaction();
            reaction.UserId = userId;
            reaction.PostId = postId;
            reaction.Type = reactionType;
            Reactiondata.reactions.Add(reaction);
            Reactiondata.SaveChanges();
        }
        public void RemoveReaction(int postId,int userId)
        {
            Reaction reaction = Reactiondata.reactions.FirstOrDefault(r =>  r.PostId == postId && r.UserId == userId);
            if (reaction == null)
                throw new Exception("Post Not Found!!");
            Reactiondata.reactions.Remove(reaction);
            Reactiondata.SaveChanges();

        }
    }
}