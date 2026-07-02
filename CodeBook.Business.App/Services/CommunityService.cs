
﻿using System;
﻿using AutoMapper.Execution;
using CodeBook.Business.App.DTOs;
using CodeBook.Business.App.Interfaces;
using CodeBook.Data.App.IRepositories;
using CodeBook.Models.App;
using System;

namespace CodeBook.Business.App.Services

{
    public class CommunityService : ICommunityService
    {
        private readonly ICommunityRepository _communityRepository;
        private readonly INotificationService _notificationService;

        public CommunityService(ICommunityRepository communityRepository,INotificationService notificationService)
        {
            _communityRepository = communityRepository;
            _notificationService = notificationService;
        }
        public void CreateCommunity(CreateCommunityDto dto,int userId)
        {
            Community community = new Community();
            community.OwnerId = userId;
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


            _notificationService.CreateNotification(community.OwnerId, new NotificationDTO
            {
                userId = community.OwnerId,
                Type = "Join",
                Message = "You have a new Community Member",
                ReferenceId = communityId,
                IsSeen = false,
                DateCreated = DateTime.UtcNow
            });

        }

        //what if I wanna Unjoin?
        public void AssignRole(int CommunityId,int userId,AssignRoleDto dto)
        {
            CommunityMember member = _communityRepository.GetCommunityMember(CommunityId, userId);

            member.Role = dto.Role;
            _communityRepository.UpdateCommunityMember(member);
            _communityRepository.SaveChanges();

        }
        public Community GetCommunity(int CommunityId)
        {
            Community community = _communityRepository.GetCommunity(CommunityId);
            return community;
        }
        public void UnjoinCommunity(int communityId,int userId)
        {
            CommunityMember member = _communityRepository.GetCommunityMember(communityId, userId);
            if(member == null)
                throw new KeyNotFoundException("Member Not Found!!");
            _communityRepository.RemoveMember(member);
            _communityRepository.SaveChanges();
        }
    }
}
