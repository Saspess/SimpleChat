using Microsoft.EntityFrameworkCore;
using SimpleChat.Data.Contexts.Contracts;
using SimpleChat.Data.Entities;
using System.Reflection;

namespace SimpleChat.Data.Contexts.Implementation
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<UserEntity> Users { get; set; } = null!;
        public DbSet<ChatEntity> Chats { get; set; } = null!;
        public DbSet<UserChatEntity> UserChats { get; set; } = null!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public async Task<int> SaveChangesAsync() => await base.SaveChangesAsync();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
