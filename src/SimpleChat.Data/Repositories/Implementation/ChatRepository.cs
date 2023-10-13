using Microsoft.EntityFrameworkCore;
using SimpleChat.Data.Contexts.Contracts;
using SimpleChat.Data.Entities;
using SimpleChat.Data.Repositories.Contracts;

namespace SimpleChat.Data.Repositories.Implementation
{
    public class ChatRepository : BaseRepository<ChatEntity>, IChatRepository
    {
        public ChatRepository(IApplicationDbContext appContext) : base(appContext)
        {
        }

        public async Task<IEnumerable<ChatEntity>> GetAllByCreatorIdAsync(int creatorId) =>
            await appContext.Chats
            .AsNoTracking()
            .Where(c => c.CreatorId == creatorId)
            .ToListAsync();
    }
}
