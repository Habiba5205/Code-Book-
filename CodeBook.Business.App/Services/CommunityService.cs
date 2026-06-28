using System;
using CodeBook.Business.App.DTOs;
using CodeBook.Business.App.Interfaces;
using CodeBook.Data.App.IRepositories;
using CodeBook.Models.App;

namespace CodeBook.Business.App.Services

{
    public class CommunityService : ICommunityService
    {
        private readonly ICommunityRepository _communityRepository;

        public CommunityService(ICommunityRepository communityRepository)
        {
            _communityRepository = communityRepository;
        }
        public void CreateCommunity(CreateCommunityDto dto)
        {
            Community community = new Community();
            community.OwnerId = dto.OwnerId;
            community.Name = dto.Name;
            community.Description = dto.Description;
            _communityRepository.Add(community);
            _communityRepository.SaveChanges();


        }
        public void UpdateCommunity(int CommunityId,UpdateCommunityDto dto)
        {
            Community community = _communityRepository.GetCommunity(CommunityId);
            community.Name = dto.Name;
            community.Description = dto.Description;
            _communityRepository.Update(community);
            _communityRepository.SaveChanges();

        }
        public void DeleteCommunity(int CommunityId)
        {
            Community community = _communityRepository.GetCommunity(CommunityId);

            _communityRepository.Delete(community);
            _communityRepository.SaveChanges();
        }
        public void JoinCommunity(int communityId,CommunityMember newMember)
        {
            //why get community?
            Community community = _communityRepository.GetCommunity(communityId);

            _communityRepository.JoinCommunity(newMember);
            _communityRepository.SaveChanges();

        }

        //what if I wanna Unjoin?
        public void AssignRole(int CommunityId,AssignRoleDto dto)
        {
            CommunityMember member = _communityRepository.GetCommunityMember(CommunityId, dto.UserId);

            member.Role = dto.Role;
            _communityRepository.UpdateCommunityMember(member);
            _communityRepository.SaveChanges();

        }
        public Community GetCommunity(int CommunityId)
        {
            Community community = _communityRepository.GetCommunity(CommunityId);
            return community;
        }
    }
}
