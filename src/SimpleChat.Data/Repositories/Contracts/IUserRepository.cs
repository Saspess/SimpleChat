using SimpleChat.Data.Entities;

namespace SimpleChat.Data.Repositories.Contracts
{
    public interface IUserRepository : IBaseRepository<UserEntity>
    {
        Task<UserEntity?> GetByUserNameAsync(string username);
        Task<UserEntity?> GetByEmailAsync(string email);
    }
}
