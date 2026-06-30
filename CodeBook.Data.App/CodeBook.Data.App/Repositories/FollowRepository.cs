using CodeBook.Data.App.IRepositories;
using CodeBook.Models.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBook.Data.App.Repositories
{
    public class FollowRepository : IFollowRepository
    {
        private readonly CodeBookContext _context;
        public FollowRepository(CodeBookContext context) { _context = context; }
        public void AddFollow(Follow follow)
        {
            _context.follows.Add(follow);
        }
        public void RemoveFollow(Follow follow)
        {
            _context.follows.Remove(follow);
        }

        public Follow GetFollow(int followerId, int followeeId)
        {
            return _context.follows.FirstOrDefault(f => f.FollowerUserId == followerId && f.FolloweeUserId == followeeId);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
