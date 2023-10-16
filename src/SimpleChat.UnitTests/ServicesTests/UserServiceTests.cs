using AutoMapper;
using Moq;
using SimpleChat.Business.DTOs.User;
using SimpleChat.Business.Exceptions;
using SimpleChat.Business.Services.Implementation;
using SimpleChat.Data.Entities;
using SimpleChat.Data.Repositories.Contracts;

namespace SimpleChat.UnitTests.ServicesTests
{
    public class UserServiceTests
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;

        private readonly UserService _userService;

        public UserServiceTests()
        {
            _mapperMock = new Mock<IMapper>();
            _userRepositoryMock = new Mock<IUserRepository>();

            _userService = new UserService(_userRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAll_WhenUsersExist_ShouldReturnSuccessResult()
        {
            //Arrange

            var entities = new List<UserEntity>()
            {
                new UserEntity()
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Username = "johndoe",
                    Email = "johndoe@gmail.com"
                },
                new UserEntity()
                {
                    Id = 2,
                    FirstName = "Julia",
                    LastName = "Doe",
                    Username = "juliadoe",
                    Email = "juliadoe@gmail.com"
                }
            };


            _userRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(entities);

            var viewDtos = new List<UserViewDto>()
            {
                new UserViewDto()
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Username = "johndoe",
                    Email = "johndoe@gmail.com"
                },
                new UserViewDto()
                {
                    Id = 2,
                    FirstName = "Julia",
                    LastName = "Doe",
                    Username = "juliadoe",
                    Email = "juliadoe@gmail.com"
                }
            };

            _mapperMock.Setup(m => m.Map<IEnumerable<UserViewDto>>(entities)).Returns(viewDtos);

            //Act

            var result = await _userService.GetAllAsync();

            //Assert

            Assert.Equal(2, result.Count());
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetById_WhenUserExists_ShouldReturnSuccessResult()
        {
            //Arrange
            var entity = new UserEntity()
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Username = "johndoe",
                Email = "johndoe@gmail.com"
            };

            _userRepositoryMock.Setup(r => r.GetByIdAsync(entity.Id)).ReturnsAsync(entity);

            var viewDto = new UserViewDto()
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Username = "johndoe",
                Email = "johndoe@gmail.com"
            };

            _mapperMock.Setup(m => m.Map<UserViewDto>(entity)).Returns(viewDto);

            //Act

            var result = await _userService.GetByIdAsync(entity.Id);

            //Assert

            Assert.Equal(result, viewDto);
        }

        [Fact]
        public async Task Update_WhenUserExists_ShouldReturnSuccessResult()
        {
            //Arrange

            var entity = new UserEntity()
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Username = "johndoe",
                Email = "johndoe@gmail.com"
            };

            _userRepositoryMock.Setup(r => r.GetByIdAsync(entity.Id)).ReturnsAsync(entity);

            var updateDto = new UserUpdateDto()
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Username = "JohnDoe",
                Email = "johndoe@gmail.com"
            };

            _mapperMock.Setup(m => m.Map<UserUpdateDto>(entity)).Returns(updateDto);

            //Act

            await _userService.UpdateAsync(updateDto);

            //Assert

            Assert.NotEqual(entity.Username, updateDto.Username);
        }

        [Fact]
        public async Task Delete_WhenUserDoNotExists_ShouldReturnFailureResult()
        {
            //Arrange

            int id = 1;

            _userRepositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(default(UserEntity));

            //Act

            var result = async () => await _userService.DeleteAsync(id);

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(result);
        }
    }
}
