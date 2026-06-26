using System;
namespace CodeBook.Business.App.Services
{
    public interface ICommunityService
    {
        void CreateCommunity(int ownerId, string name,string description);
        void JoinCommunity(int communityId,CommunityMember newMember);
        void AssignRole(int communityId, int userId, CommunityRole role);
        void UpdateCommunity(int communityId, string name, string description);
        void DeleteCommunity(int communityId);

    }
}
