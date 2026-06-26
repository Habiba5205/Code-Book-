using System;
using CodeBook.Business.App.Interfaces;
using CodeBook.Data.App;
using CodeBook.Models.App;

namespace CodeBook.Business.App.Services

{
    public class CommunityService : ICommunityService
    {
        private CodeBookContext CommunityData;

        public CommunityService(CodeBookContext CommunityData)
        {
            this.CommunityData = CommunityData;
        }
        public void CreateCommunity(int OwnerId,string name,string description)
        {
            Community community = new Community();
            community.OwnerId = OwnerId;
            community.Name = name;
            community.Description = description;
            CommunityData.communities.Add(community);
            CommunityData.SaveChanges();


        }
        public void UpdateCommunity(int communityId,string name,string description)
        {
            Community community = CommunityData.communities.FirstOrDefault(c => c.Id == communityId);
            if (community == null)
                throw new Exception("Community Not Found!!");
            community.Name = name;
            community.Description = description;
            CommunityData.SaveChanges();

        }
        public void DeleteCommunity(int communityId)
        {
            Community community = CommunityData.communities.FirstOrDefault(c => c.Id == communityId);
            if (community == null)
                throw new Exception("Community Not Found!!");

            CommunityData.communities.Remove(community);
            CommunityData.SaveChanges();



        }
        public void JoinCommunity(int communityId,CommunityMember newMember)
        {
            Community community = CommunityData.communities.FirstOrDefault(c => c.Id == communityId);
            if (community == null)
                throw new Exception("Community Not Found!!");

            community.communityMembers.Add(newMember);
            CommunityData.SaveChanges();

        }
        public void AssignRole(int communityId,int userId,CommunityRole Role)
        {
            CommunityMember member = CommunityData.communityMembers.FirstOrDefault(m  => m.CommunityId == communityId && m.UserId == userId);
       
            if (member == null)
                throw new Exception("Member Not Found!!");

            member.Role = Role;
            CommunityData.SaveChanges();


        }
    }
}
