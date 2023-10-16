using AutoMapper;
using SimpleChat.Business.DTOs.User;
using SimpleChat.Data.Entities;

namespace SimpleChat.Business.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserEntity, UserViewDto>();

            CreateMap<UserCreateDto, UserEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.UserChats, opt => opt.Ignore());

            CreateMap<UserUpdateDto, UserEntity>()
                .ForMember(dest => dest.UserChats, opt => opt.Ignore());
        }
    }
}
