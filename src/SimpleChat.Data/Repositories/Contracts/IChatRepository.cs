using SimpleChat.Data.Entities;

namespace SimpleChat.Data.Repositories.Contracts
{
    public interface IChatRepository : IBaseRepository<ChatEntity>
    {
        Task<IEnumerable<ChatEntity>> GetAllByCreatorIdAsync(int creatorId);
    }
}
