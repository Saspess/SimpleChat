﻿using SimpleChat.Business.DTOs.UserChat;

namespace SimpleChat.Business.Services.Contracts
{
    public interface IUserChatService
    {
        Task<IEnumerable<ChatUserViewDto>> GetAllUsersByChatIdAsync(int chatId);
        Task<IEnumerable<UserChatViewDto>> GetAllChatsByUserIdAsync(int userId);
        Task<UserChatViewDto> GetByUserIdAndChatIdAsync(int userId, int chatId);
        Task<ChatUserViewDto> AddToChatAsync(UserChatCreateDto userChatCreateDto);
        Task DeleteFromChatAsync(int id);
    }
}
