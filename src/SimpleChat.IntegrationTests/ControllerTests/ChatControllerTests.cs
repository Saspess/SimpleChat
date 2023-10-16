using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleChat.Business.DTOs.Chat;
using SimpleChat.Business.DTOs.User;
using SimpleChat.Data.Contexts.Implementation;
using SimpleChat.Data.Entities;
using System.Net;
using System.Net.Http.Json;

namespace SimpleChat.IntegrationTests.ControllerTests
{
    public class ChatControllerTests
    {
        [Fact]
        public async Task GetAll_WhenChatsExist_ShouldReturnOk()
        {
            // Arrange

            var webHost = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var dbContextDescriptor = services.FirstOrDefault(d =>
                        d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                    services.Remove(dbContextDescriptor);

                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("ChatGetAllDb");
                    });
                });
            });

            var dbContext =
                webHost.Services.CreateScope().ServiceProvider.GetService<ApplicationDbContext>();

            var chats = new List<ChatEntity>()
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

            await dbContext.Chats.AddRangeAsync(chats);

            await dbContext.SaveChangesAsync();

            var httpClient = webHost.CreateClient();

            // Act

            var response = await httpClient.GetAsync("api/Chat");

            // Assert

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetAllByCreatorId_WhenChatsExist_ShouldReturnOk()
        {
            // Arrange

            var webHost = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var dbContextDescriptor = services.FirstOrDefault(d =>
                        d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                    services.Remove(dbContextDescriptor);

                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("ChatGetAllByCreatorIdDb");
                    });
                });
            });

            var dbContext =
                webHost.Services.CreateScope().ServiceProvider.GetService<ApplicationDbContext>();

            var chats = new List<ChatEntity>()
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

            await dbContext.Chats.AddRangeAsync(chats);

            await dbContext.SaveChangesAsync();

            var httpClient = webHost.CreateClient();

            var creatorId = 1;

            // Act

            var response = await httpClient.GetAsync($"api/Chat/get-by-creatorId/{creatorId}");

            // Assert

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetAllByName_WhenChatsExist_ShouldReturnOk()
        {
            // Arrange

            var webHost = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var dbContextDescriptor = services.FirstOrDefault(d =>
                        d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                    services.Remove(dbContextDescriptor);

                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("ChatGetAllByNameDb");
                    });
                });
            });

            var dbContext =
                webHost.Services.CreateScope().ServiceProvider.GetService<ApplicationDbContext>();

            var chats = new List<ChatEntity>()
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

            await dbContext.Chats.AddRangeAsync(chats);

            await dbContext.SaveChangesAsync();

            var httpClient = webHost.CreateClient();

            var name = "TestChat1";

            // Act

            var response = await httpClient.GetAsync($"api/Chat/get-by-name/{name}");

            // Assert

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetById_WhenChatExists_ShouldReturnOk()
        {
            // Arrange

            var webHost = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var dbContextDescriptor = services.FirstOrDefault(d =>
                        d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                    services.Remove(dbContextDescriptor);

                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("ChatGetByIdDb");
                    });
                });
            });

            var dbContext =
                webHost.Services.CreateScope().ServiceProvider.GetService<ApplicationDbContext>();

            var chats = new List<ChatEntity>()
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

            await dbContext.Chats.AddRangeAsync(chats);

            await dbContext.SaveChangesAsync();

            var httpClient = webHost.CreateClient();

            var id = 1;

            // Act

            var response = await httpClient.GetAsync($"api/Chat/{id}");

            // Assert

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Create_WhenChatDoNotExists_ShouldReturnCreated()
        {
            // Arrange

            var webHost = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var dbContextDescriptor = services.FirstOrDefault(d =>
                        d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                    services.Remove(dbContextDescriptor);

                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("ChatCreateDb");
                    });
                });
            });

            var dbContext =
                webHost.Services.CreateScope().ServiceProvider.GetService<ApplicationDbContext>();

            var user = new UserEntity()
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Username = "johndoe",
                Email = "johndoe@gmail.com"
            };

            await dbContext.Users.AddAsync(user);

            await dbContext.SaveChangesAsync();

            var httpClient = webHost.CreateClient();

            var chatCreateDto = new ChatCreateDto()
            {
                Name = "TestChat3",
                Description = "Third test chat",
                CreatorId = 1
            };

            var content = JsonContent.Create(chatCreateDto);

            // Act

            var response = await httpClient.PostAsync("api/Chat/", content);

            // Assert

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task Update_WhenChatExists_ShouldReturnOk()
        {
            // Arrange

            var webHost = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var dbContextDescriptor = services.FirstOrDefault(d =>
                        d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                    services.Remove(dbContextDescriptor);

                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("ChatUpdateDb");
                    });
                });
            });

            var dbContext =
                webHost.Services.CreateScope().ServiceProvider.GetService<ApplicationDbContext>();

            var chats = new List<ChatEntity>()
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

            await dbContext.Chats.AddRangeAsync(chats);

            await dbContext.SaveChangesAsync();

            var httpClient = webHost.CreateClient();

            var chatUpdateDto = new ChatUpdateDto()
            {
                Id = 1,
                Name = "TestChat1",
                Description = "First test chat",
                CreatorId = 1
            };

            var content = JsonContent.Create(chatUpdateDto);

            // Act

            var response = await httpClient.PutAsync("api/Chat/", content);

            // Assert

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
