using System;
using CodeBook.Business.App.Services;
using CodeBook.Data.App;
using CodeBook.Models.App;


namespace CodeBook.Business.App.Methods
{
    public class ReactionService : IReactionService
    {
        private CodeBookContext Reactiondata;

        public ReactionService(CodeBookContext reactionData ) 
        {
            Reactiondata = reactionData;
        }
        public void Add(int userId,int postId,ReactionType reactionType)
        {
            Reaction reaction = new Reaction();
            reaction.UserId = userId;
            reaction.PostId = postId;
            reaction.Type = reactionType;
            Reactiondata.Reactions.Add(reaction);
            Reactiondata.SaveChanges();
        }
        public void Remove(int postId,int userId)
        {
            Reaction reaction = Reactiondata.Reactions.FirstOrDefault(r =>  r.PostId == postId && r.UserId == userId);
            if (reaction == null)
                throw new Exception("Post Not Found!!");
            Reactiondata.Reactions.Remove(reaction);
            Reactiondata.SaveChanges();

        }
    }
}