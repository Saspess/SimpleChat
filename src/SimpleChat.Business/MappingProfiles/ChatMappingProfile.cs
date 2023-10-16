using AutoMapper;
using SimpleChat.Business.DTOs.Chat;
using SimpleChat.Data.Entities;

namespace SimpleChat.Business.MappingProfiles
{
    public class ChatMappingProfile : Profile
    {
        public ChatMappingProfile()
        {
            CreateMap<ChatEntity, ChatViewDto>();

            CreateMap<ChatCreateDto, ChatEntity>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ChatUsers, opt => opt.Ignore());

            CreateMap<ChatUpdateDto, ChatEntity>()
                .ForMember(dest => dest.ChatUsers, opt => opt.Ignore());
        }
    }
}
