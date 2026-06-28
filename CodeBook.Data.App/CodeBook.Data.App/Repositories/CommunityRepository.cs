using CodeBook.Data.App.IRepositories;
using CodeBook.Models.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBook.Data.App.Repositories
{
    public class CommunityRepository : ICommunityRepository
    {
        private readonly CodeBookContext _context;
        public CommunityRepository(CodeBookContext context)
        {
            _context = context;
        }

        public Community GetCommunity(int communityId)
        {
            Community community = _context.communities.FirstOrDefault(c => c.Id == communityId);
            if (community == null)
                throw new Exception("Community Not Found!!");
            return community;
        }

        public void Add(Community community)
        {
            _context.communities.Add(community);
        }

        public void Update(Community community)
        {
            _context.communities.Update(community);
        }

        public void Delete(Community community)
        {
            _context.communities.Remove(community);
        }

        public void JoinCommunity(CommunityMember member)
        {
            _context.communityMembers.Add(member);
        }
        public CommunityMember GetCommunityMember(int communityid, int userid)
        {
            CommunityMember member = _context.communityMembers.FirstOrDefault(m => m.CommunityId == communityid && m.UserId == userid);

            if (member == null)
                throw new Exception("Member Not Found!!");
            return member;
        }

        public void UpdateCommunityMember(CommunityMember member)
        {
            _context.communityMembers.Update(member);
        }
        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
