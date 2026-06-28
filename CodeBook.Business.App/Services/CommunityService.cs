
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


            _notificationService.CreateNotification(community.OwnerId, new NotificationDTO
            {
                UserId = community.OwnerId,
                Type = "Join",
                Message = "You have a new Community Member",
                ReferenceId = communityId,
                IsSeen = false,
                DateCreated = DateTime.UtcNow
            });

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
<<<<<<< Updated upstream
=======
<<<<<<< HEAD

=======
>>>>>>> Stashed changes
        public void UnjoinCommunity(int communityId,UnjoinCommunityDto dto)
        {
            CommunityMember member = _communityRepository.GetCommunityMember(communityId, dto.userId);
            if(member == null)
                throw new KeyNotFoundException("Member Not Found!!");
            _communityRepository.RemoveMember(member);
            _communityRepository.SaveChanges();
        }
<<<<<<< Updated upstream
=======
>>>>>>> 9d773f7a06e2c4d5987595c38e7d6eb5f9451a6e
>>>>>>> Stashed changes
    }
}
