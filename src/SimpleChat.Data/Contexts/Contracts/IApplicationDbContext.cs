using Microsoft.EntityFrameworkCore;
using SimpleChat.Data.Entities;

namespace SimpleChat.Data.Contexts.Contracts
{
    public interface IApplicationDbContext
    {
        DbSet<UserEntity> Users { get; set; }
        DbSet<ChatEntity> Chats { get; set; }
        DbSet<UserChatEntity> UserChats { get; set; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        Task<int> SaveChangesAsync();
    }
}
