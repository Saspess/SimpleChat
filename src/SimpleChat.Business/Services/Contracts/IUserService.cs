using SimpleChat.Business.DTOs.User;

namespace SimpleChat.Business.Services.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserViewDto>> GetAllAsync();
        Task<UserViewDto> GetByIdAsync(int id);
        Task<UserViewDto> GetByEmailAsync(string email);
        Task<UserViewDto> GetByUsernameAsync(string username);
        Task<UserViewDto> CreateAsync(UserCreateDto userCreateDto);
        Task UpdateAsync(UserUpdateDto userUpdateDto);
        Task DeleteAsync(int id);
    }
}
