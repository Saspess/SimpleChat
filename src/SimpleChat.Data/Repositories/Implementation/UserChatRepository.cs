using Microsoft.EntityFrameworkCore;
using SimpleChat.Data.Contexts.Contracts;
using SimpleChat.Data.Entities;
using SimpleChat.Data.Repositories.Contracts;

namespace SimpleChat.Data.Repositories.Implementation
{
    public class UserChatRepository : BaseRepository<UserChatEntity>, IUserChatRepository
    {
        public UserChatRepository(IApplicationDbContext appContext) : base(appContext)
        {
        }

        public async Task<IEnumerable<UserChatEntity>> GetAllUserChatsAsync(int userId) =>
            await appContext.UserChats
            .AsNoTracking()
            .Where(uc => uc.UserId == userId)
            .Include(uc => uc.Chat)
            .ToListAsync();

        public async Task<IEnumerable<UserChatEntity>> GetAllChatUsersAsync(int chatId) =>
            await appContext.UserChats
            .AsNoTracking()
            .Where(uc => uc.ChatId == chatId)
            .Include(uc => uc.User)
            .ToListAsync();

        public override async Task<UserChatEntity?> GetByIdAsync(int id) =>
            await appContext.UserChats
            .AsNoTracking()
            .Include(uc => uc.User)
            .FirstOrDefaultAsync(uc => uc.Id == id);
    }
}
