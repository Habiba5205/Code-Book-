using System;
using CodeBook.Business.App.DTOs;
using CodeBook.Models.App;
namespace CodeBook.Business.App.Interfaces
{
    public interface ICommunityService
    {
        void CreateCommunity(CreateCommunityDto dto,int userId);
        void JoinCommunity(int communityId, CommunityMember newMember);
        void AssignRole(int CommunityId, AssignRoleDto dto);
        void UpdateCommunity(int CommunityId,UpdateCommunityDto dto);
        void DeleteCommunity(int CommunityId);
        Community GetCommunity(int CommunityId);
        void UnjoinCommunity(int communityId, int userId);

    }
}
