using Microsoft.EntityFrameworkCore;
using SimpleChat.Data.Contexts.Contracts;
using SimpleChat.Data.Entities;
using SimpleChat.Data.Repositories.Contracts;

namespace SimpleChat.Data.Repositories.Implementation
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        public UserRepository(IApplicationDbContext appContext) : base(appContext)
        {
        }

        public async Task<UserEntity?> GetByUserNameAsync(string username) =>
            await appContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.UserName == username);

        public async Task<UserEntity?> GetByEmailAsync(string email) =>
            await appContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email);
    }
}
