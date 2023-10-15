using AutoMapper;
using SimpleChat.Business.DTOs.Chat;
using SimpleChat.Business.DTOs.UserChat;
using SimpleChat.Business.Exceptions;
using SimpleChat.Business.Services.Contracts;
using SimpleChat.Data.Entities;
using SimpleChat.Data.Repositories.Contracts;
using SimpleChat.Data.Repositories.Implementation;

namespace SimpleChat.Business.Services.Implementation
{
    public class UserChatService : IUserChatService
    {
        private readonly IUserChatRepository _userChatRepository;
        private readonly IChatRepository _chatRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserChatService(IUserChatRepository userChatRepository, IChatRepository chatRepository, IUserRepository userRepository, IMapper mapper)
        {
            _userChatRepository = userChatRepository ?? throw new ArgumentNullException(nameof(userChatRepository));
            _chatRepository = chatRepository ?? throw new ArgumentNullException(nameof(chatRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<ChatUserViewDto>> GetAllUsersByChatIdAsync(int chatId)
        {
            var existingUserEntity = await _chatRepository.GetByIdAsync(chatId)
                ?? throw new NotFoundException("Chat was not found.");

            var userChatEntities = await _userChatRepository.GetAllChatUsersAsync(chatId);
            var chatUserViewDtos = _mapper.Map<IEnumerable<ChatUserViewDto>>(userChatEntities);

            return chatUserViewDtos;
        }

        public async Task<IEnumerable<UserChatViewDto>> GetAllChatsByUserIdAsync(int userId)
        {
            var existingUserEntity = await _userChatRepository.GetByIdAsync(userId)
                ?? throw new NotFoundException("User was not found.");

            var userChatEntities = await _userChatRepository.GetAllUserChatsAsync(userId);
            var userChatViewDtos = _mapper.Map<IEnumerable<UserChatViewDto>>(userChatEntities);

            return userChatViewDtos;
        }

        public async Task<ChatUserViewDto> AddToChatAsync(UserChatCreateDto userChatCreateDto)
        {
            ArgumentNullException.ThrowIfNull(userChatCreateDto, nameof(userChatCreateDto));

            var existingUserEntity = await _userRepository.GetByIdAsync(userChatCreateDto.UserId)
                ?? throw new NotFoundException("User was not found.");

            var existingChatEntity = await _chatRepository.GetByIdAsync(userChatCreateDto.ChatId)
                ?? throw new NotFoundException("Chat was not found.");

            var userChatEntity = _mapper.Map<UserChatEntity>(userChatCreateDto);

            var createdChatUserEntity = await _userChatRepository.CreateAsync(userChatEntity);
            var userChatViewDto = _mapper.Map<ChatUserViewDto>(createdChatUserEntity);

            return userChatViewDto;
        }

        public async Task DeleteFromChatAsync(int id)
        {
            var existingUserChatEntity = await _userChatRepository.GetByIdAsync(id)
                ?? throw new NotFoundException("User chat was not found.");

            await _userChatRepository.DeleteAsync(id);
        }
    }
}
