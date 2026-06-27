using System;
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
        public void CreateCommunity(int OwnerId,string name,string description)
        {
            Community community = new Community();
            community.OwnerId = OwnerId;
            community.Name = name;
            community.Description = description;
            _communityRepository.Add(community);
            _communityRepository.SaveChanges();


        }
        public void UpdateCommunity(int communityId,string name,string description)
        {
            Community community = _communityRepository.GetCommunity(communityId);
            community.Name = name;
            community.Description = description;
            _communityRepository.Update(community);
            _communityRepository.SaveChanges();

        }
        public void DeleteCommunity(int communityId)
        {
            Community community = _communityRepository.GetCommunity(communityId);

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
        public void AssignRole(int communityId,int userId,CommunityRole Role)
        {
            CommunityMember member = _communityRepository.GetCommunityMember(communityId, userId);

            member.Role = Role;
            _communityRepository.UpdateCommunityMember(member);
            _communityRepository.SaveChanges();

        }
    }
}
