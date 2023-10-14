using SimpleChat.Business.DTOs.Chat;

namespace SimpleChat.Business.Services.Contracts
{
    public interface IChatService
    {
        Task<IEnumerable<ChatViewDto>> GetAllAsync();
        Task<IEnumerable<ChatViewDto>> GetAllByCreatorIdAsync(int creatorId);
        Task<ChatViewDto> GetByIdAsync(int id);
        Task<ChatViewDto> CreateAsync(ChatCreateDto chatCreateDto);
        Task UpdateAsync(ChatUpdateDto chatUpdateDto);
        Task DeleteAsync(ChatDeleteDto chatDeleteDto);
    }
}
