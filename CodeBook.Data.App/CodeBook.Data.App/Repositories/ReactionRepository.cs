using CodeBook.Data.App.IRepositories;
using CodeBook.Models.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBook.Data.App.Repositories
{
    public class ReactionRepository : IReactionRepository
    {
        private readonly CodeBookContext _context;
        public ReactionRepository(CodeBookContext context)
        {
            _context = context;

        }
        public Reaction GetReaction(int postId, int userId)
        {
            Reaction reaction = _context.reactions.FirstOrDefault(r => r.PostId == postId && r.UserId == userId);
            if (reaction == null)
                throw new Exception("Reaction Not Found!!");
            return reaction;
        }

        public void Add(Reaction reaction)
        {
            _context.reactions.Add(reaction);
        }

        public void Remove(Reaction reaction)
        {
            _context.reactions.Remove(reaction);
        }
        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
