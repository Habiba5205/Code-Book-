using System;
using AutoMapper;
using CodeBook.Business.App.DTOs;
using CodeBook.Models.App;

namespace CodeBook.Business.App.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // map notification to notificationDTO
            CreateMap<Notification, NotificationDTO>()
               .ForMember(dest => dest.Type,
                          opt => opt.MapFrom(src => src.Type.ToString()))
               .ForMember(dest => dest.IsSeen,
                          opt => opt.MapFrom(src => src.IsSeen))
               .ForMember(dest => dest.DateCreated,
                          opt => opt.MapFrom(src => src.DateCreated));

            CreateMap<Report, ReportDTO>()
                .ForMember(dest => dest.Status,
                            opt => opt.MapFrom(src => src.Status.ToString()));

            CreateMap<Post, PostResponse>()
                .ForMember(dest => dest.AuthorUsername,
                           opt => opt.MapFrom(src => src.Author.UserName));

            CreateMap<User, UserProfileResponse>()
                .ForMember(dest => dest.UserName,
                           opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Bio,
                           opt => opt.MapFrom(src => src.Bio))
                .ForMember(dest => dest.AvatarUrl,
                            opt => opt.MapFrom(src => src.AvatarUrl))
                .ForMember(dest => dest.JoinedAt,
                            opt => opt.MapFrom(src => src.DateCreated));

            CreateMap<UpdateProfileDto, User>()
                .ForMember(dest => dest.Bio,
                           opt => opt.MapFrom(src => src.Bio))
                .ForMember(dest => dest.AvatarUrl,
                           opt => opt.MapFrom(src => src.AvatarUrl));
        }

    }
}
