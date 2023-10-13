using SimpleChat.Data.Entities;

namespace SimpleChat.Data.Repositories.Contracts
{
    public interface IUserChatRepository : IBaseRepository<UserChatEntity>
    {
        Task<IEnumerable<UserChatEntity>> GetAllUserChatsAsync(int userId);
        Task<IEnumerable<UserChatEntity>> GetAllChatUsersAsync(int chatId);
    }
}
