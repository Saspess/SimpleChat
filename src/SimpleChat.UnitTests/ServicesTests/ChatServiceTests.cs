using AutoMapper;
using Moq;
using SimpleChat.Business.DTOs.Chat;
using SimpleChat.Business.DTOs.User;
using SimpleChat.Business.Exceptions;
using SimpleChat.Business.Services.Contracts;
using SimpleChat.Business.Services.Implementation;
using SimpleChat.Data.Entities;
using SimpleChat.Data.Repositories.Contracts;

namespace SimpleChat.UnitTests.ServicesTests
{
    public class ChatServiceTests
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IChatRepository> _chatRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IUserChatRepository> _userChatRepositoryMock;

        private readonly ChatService _chatService;

        public ChatServiceTests()
        {
            _mapperMock = new Mock<IMapper>();
            _chatRepositoryMock = new Mock<IChatRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _userChatRepositoryMock = new Mock<IUserChatRepository>();

            _chatService = new ChatService(_chatRepositoryMock.Object, _userRepositoryMock.Object, _userChatRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAll_WhenChatsExist_ShouldReturnSuccessResult()
        {
            //Arrange

            var entities = new List<ChatEntity>()
            {
                new ChatEntity()
                {
                    Id = 1,
                    Name = "TestChat1",
                    Description = "First test chat",
                    CreatorId = 1
                },
                new ChatEntity()
                {
                    Id = 2,
                    Name = "TestChat2",
                    Description = "Second test chat",
                    CreatorId = 1
                }
            };


            _chatRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(entities);

            var viewDtos = new List<ChatViewDto>()
            {
                new ChatViewDto()
                {
                    Id = 1,
                    Name = "TestChat1",
                    Description = "First test chat",
                    CreatorId = 1
                },
                new ChatViewDto()
                {
                    Id = 2,
                    Name = "TestChat2",
                    Description = "Second test chat",
                    CreatorId = 1
                }
            };

            _mapperMock.Setup(m => m.Map<IEnumerable<ChatViewDto>>(entities)).Returns(viewDtos);

            //Act

            var result = await _chatService.GetAllAsync();

            //Assert

            Assert.Equal(2, result.Count());
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetById_WhenChatExists_ShouldReturnSuccessResult()
        {
            //Arrange

            var entity = new ChatEntity()
            {
                Id = 1,
                Name = "TestChat1",
                Description = "First test chat",
                CreatorId = 1
            };

            _chatRepositoryMock.Setup(r => r.GetByIdAsync(entity.Id)).ReturnsAsync(entity);

            var viewDto = new ChatViewDto()
            {
                Id = 1,
                Name = "TestChat1",
                Description = "First test chat",
                CreatorId = 1
            };

            _mapperMock.Setup(m => m.Map<ChatViewDto>(entity)).Returns(viewDto);

            //Act

            var result = await _chatService.GetByIdAsync(entity.Id);

            //Assert

            Assert.Equal(result, viewDto);
        }

        [Fact]
        public async Task Update_WhenChatExists_ShouldReturnSuccessResult()
        {
            //Arrange

            var entity = new ChatEntity()
            {
                Id = 1,
                Name = "TestChat1",
                Description = "First test chat",
                CreatorId = 1
            };

            _chatRepositoryMock.Setup(r => r.GetByIdAsync(entity.Id)).ReturnsAsync(entity);

            var userEntity = new UserEntity()
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Username = "johndoe",
                Email = "johndoe@gmail.com"
            };

            _userRepositoryMock.Setup(r => r.GetByIdAsync(userEntity.Id)).ReturnsAsync(userEntity);

            var updateDto = new ChatUpdateDto()
            {
                Id = 1,
                Name = "TestChat",
                Description = "First test chat",
                CreatorId = 1
            };

            _mapperMock.Setup(m => m.Map<ChatUpdateDto>(entity)).Returns(updateDto);

            //Act

            await _chatService.UpdateAsync(updateDto);

            //Assert

            Assert.NotEqual(entity.Name, updateDto.Name);
        }

        [Fact]
        public async Task Delete_WhenUserDoNotExists_ShouldReturnFailureResult()
        {
            //Arrange

            var chatDeleteDto = new ChatDeleteDto()
            {
                Id = 1,
                CreatorId = 1
            };

            var userEntity = new UserEntity()
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Username = "johndoe",
                Email = "johndoe@gmail.com"
            };

            _userRepositoryMock.Setup(r => r.GetByIdAsync(chatDeleteDto.CreatorId)).ReturnsAsync(userEntity);

            _chatRepositoryMock.Setup(r => r.GetByIdAsync(chatDeleteDto.Id)).ReturnsAsync(default(ChatEntity));

            //Act

            var result = async () => await _chatService.DeleteAsync(chatDeleteDto);

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(result);
        }
    }
}
