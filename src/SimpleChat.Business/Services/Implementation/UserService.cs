using AutoMapper;
using SimpleChat.Business.DTOs.Chat;
using SimpleChat.Business.DTOs.User;
using SimpleChat.Business.Exceptions;
using SimpleChat.Business.Services.Contracts;
using SimpleChat.Data.Entities;
using SimpleChat.Data.Repositories.Contracts;
using SimpleChat.Data.Repositories.Implementation;

namespace SimpleChat.Business.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<UserViewDto>> GetAllAsync()
        {
            var userEntities = await _userRepository.GetAllAsync();
            var userViewDtos = _mapper.Map<IEnumerable<UserViewDto>>(userEntities);

            return userViewDtos;
        }

        public async Task<UserViewDto> GetByIdAsync(int id)
        {
            var userEntity = await _userRepository.GetByIdAsync(id)
                ?? throw new NotFoundException("User was not found.");

            var userViewDto = _mapper.Map<UserViewDto>(userEntity);

            return userViewDto;
        }

        public async Task<UserViewDto> GetByEmailAsync(string email)
        {
            var userEntity = await _userRepository.GetByEmailAsync(email)
                ?? throw new NotFoundException("User was not found.");

            var userViewDto = _mapper.Map<UserViewDto>(userEntity);

            return userViewDto;
        }

        public async Task<UserViewDto> GetByUsernameAsync(string username)
        {
            var userEntity = await _userRepository.GetByUsernameAsync(username)
                ?? throw new NotFoundException("User was not found.");

            var userViewDto = _mapper.Map<UserViewDto>(userEntity);

            return userViewDto;
        }

        public async Task<UserViewDto> CreateAsync(UserCreateDto userCreateDto)
        {
            ArgumentNullException.ThrowIfNull(userCreateDto, nameof(userCreateDto));

            var userEntity = _mapper.Map<UserEntity>(userCreateDto);

            var createdUserEntity = await _userRepository.CreateAsync(userEntity);
            var userViewDto = _mapper.Map<UserViewDto>(createdUserEntity);

            return userViewDto;
        }

        public async Task UpdateAsync(UserUpdateDto userUpdateDto)
        {
            ArgumentNullException.ThrowIfNull(userUpdateDto, nameof(userUpdateDto));

            var existingUserEntity = await _userRepository.GetByIdAsync(userUpdateDto.Id)
                ?? throw new NotFoundException("User was not found.");

            var userEntity = _mapper.Map<UserEntity>(userUpdateDto);

            await _userRepository.UpdateAsync(userEntity);
        }

        public async Task DeleteAsync(int id)
        {
            var existingUserEntity = await _userRepository.GetByIdAsync(id)
                ?? throw new NotFoundException("User was not found.");

            await _userRepository.DeleteAsync(id);
        }
    }
}
