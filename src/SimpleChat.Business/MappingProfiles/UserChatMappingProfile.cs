using AutoMapper;
using SimpleChat.Business.DTOs.UserChat;
using SimpleChat.Data.Entities;

namespace SimpleChat.Business.MappingProfiles
{
    public class UserChatMappingProfile : Profile
    {
        public UserChatMappingProfile()
        {
            CreateMap<UserChatEntity, UserChatViewDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Chat.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Chat.Description))
                .ForMember(dest => dest.CreatorId, opt => opt.MapFrom(src => src.Chat.CreatorId))
                .ForMember(dest => dest.ChatId, opt => opt.MapFrom(src => src.Chat.Id));

            CreateMap<UserChatEntity, ChatUserViewDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Username))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id));

            CreateMap<UserChatCreateDto, UserChatEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Chat, opt => opt.Ignore());
        }
    }
}
