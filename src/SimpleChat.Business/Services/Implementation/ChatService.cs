using AutoMapper;
using SimpleChat.Business.DTOs.Chat;
using SimpleChat.Business.Exceptions;
using SimpleChat.Business.Services.Contracts;
using SimpleChat.Data.Entities;
using SimpleChat.Data.Repositories.Contracts;

namespace SimpleChat.Business.Services.Implementation
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserChatRepository _userChatRepository;
        private readonly IMapper _mapper;

        public ChatService(IChatRepository chatRepository, IUserRepository userRepository, IUserChatRepository userChatRepository, IMapper mapper)
        {
            _chatRepository = chatRepository ?? throw new ArgumentNullException(nameof(chatRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _userChatRepository = userChatRepository ?? throw new ArgumentNullException(nameof(userChatRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<ChatViewDto>> GetAllAsync()
        {
            var chatEntities = await _chatRepository.GetAllAsync();
            var chatViewDtos = _mapper.Map<IEnumerable<ChatViewDto>>(chatEntities);

            return chatViewDtos;
        }

        public async Task<IEnumerable<ChatViewDto>> GetAllByCreatorIdAsync(int creatorId)
        {
            var chatEntities = await _chatRepository.GetAllByCreatorIdAsync(creatorId);
            var chatViewDtos = _mapper.Map<IEnumerable<ChatViewDto>>(chatEntities);

            return chatViewDtos;
        }

        public async Task<IEnumerable<ChatViewDto>> GetAllByNameAsync(string name)
        {
            var chatEntities = await _chatRepository.GetAllByNameAsync(name);
            var chatViewDtos = _mapper.Map<IEnumerable<ChatViewDto>>(chatEntities);

            return chatViewDtos;
        }

        public async Task<ChatViewDto> GetByIdAsync(int id)
        {
            var chatEntity = await _chatRepository.GetByIdAsync(id)
                ?? throw new NotFoundException("Chat was not found.");

            var chatViewDto = _mapper.Map<ChatViewDto>(chatEntity);

            return chatViewDto;
        }

        public async Task<ChatViewDto> CreateAsync(ChatCreateDto chatCreateDto)
        {
            ArgumentNullException.ThrowIfNull(chatCreateDto, nameof(chatCreateDto));

            var existingUserEntity = await _userRepository.GetByIdAsync(chatCreateDto.CreatorId)
                ?? throw new NotFoundException("Creator was not found.");

            var chatEntity = _mapper.Map<ChatEntity>(chatCreateDto);

            var createdChatEntity = await _chatRepository.CreateAsync(chatEntity);

            await _userChatRepository.CreateAsync(new UserChatEntity 
            { 
                ChatId = createdChatEntity.Id, 
                UserId = chatCreateDto.CreatorId
            });

            var chatViewDto = _mapper.Map<ChatViewDto>(createdChatEntity);

            return chatViewDto;
        }

        public async Task UpdateAsync(ChatUpdateDto chatUpdateDto)
        {
            ArgumentNullException.ThrowIfNull(chatUpdateDto, nameof(chatUpdateDto));

            var existingUserEntity = _userRepository.GetByIdAsync(chatUpdateDto.CreatorId)
                ?? throw new NotFoundException("Creator was not found.");

            var existingChatEntity = await _chatRepository.GetByIdAsync(chatUpdateDto.Id)
                ?? throw new NotFoundException("Chat was not found.");

            var chatEntity = _mapper.Map<ChatEntity>(chatUpdateDto);

            await _chatRepository.UpdateAsync(chatEntity);
        }

        public async Task DeleteAsync(ChatDeleteDto chatDeleteDto)
        {
            ArgumentNullException.ThrowIfNull(chatDeleteDto, nameof(chatDeleteDto));

            var existingChatEntity = await _chatRepository.GetByIdAsync(chatDeleteDto.Id)
                ?? throw new NotFoundException("Chat was not found.");

            var existingUserEntity = _userRepository.GetByIdAsync(chatDeleteDto.CreatorId)
                ?? throw new NotFoundException("Creator was not found.");

            if (existingChatEntity.CreatorId != chatDeleteDto.CreatorId)
            {
                throw new AccessDeniedException();
            }

            await _chatRepository.DeleteAsync(chatDeleteDto.Id);
        }
    }
}
