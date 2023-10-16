using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleChat.Business.DTOs.User;
using SimpleChat.Data.Contexts.Implementation;
using SimpleChat.Data.Entities;
using System.Net;
using System.Net.Http.Json;

namespace SimpleChat.IntegrationTests.ControllerTests
{
    public class UserControllerTests
    {
        [Fact]
        public async Task GetAll_WhenUsersExist_ShouldReturnOk()
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
                        options.UseInMemoryDatabase("UserGetAllDb");
                    });
                });
            });

            var dbContext =
                webHost.Services.CreateScope().ServiceProvider.GetService<ApplicationDbContext>();

            var users = new List<UserEntity>() 
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

            await dbContext.Users.AddRangeAsync(users);

            await dbContext.SaveChangesAsync();

            var httpClient = webHost.CreateClient();

            // Act

            var response = await httpClient.GetAsync("api/User");

            // Assert

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetById_WhenUserExists_ShouldReturnOk()
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
                        options.UseInMemoryDatabase("UserGetByIdDb");
                    });
                });
            });

            var dbContext =
                webHost.Services.CreateScope().ServiceProvider.GetService<ApplicationDbContext>();

            var users = new List<UserEntity>()
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

            await dbContext.Users.AddRangeAsync(users);

            await dbContext.SaveChangesAsync();

            var httpClient = webHost.CreateClient();

            var id = 1;

            // Act

            var response = await httpClient.GetAsync($"api/User/{id}");

            // Assert

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetByEmail_WhenUserExists_ShouldReturnOk()
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
                        options.UseInMemoryDatabase("UserGetByEmailDb");
                    });
                });
            });

            var dbContext =
                webHost.Services.CreateScope().ServiceProvider.GetService<ApplicationDbContext>();

            var users = new List<UserEntity>()
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

            await dbContext.Users.AddRangeAsync(users);

            await dbContext.SaveChangesAsync();

            var httpClient = webHost.CreateClient();

            var email = "johndoe@gmail.com";

            // Act

            var response = await httpClient.GetAsync($"api/User/get-by-email/{email}");

            // Assert

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetByUsername_WhenUserExists_ShouldReturnOk()
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
                        options.UseInMemoryDatabase("UserGetByUsernameDb");
                    });
                });
            });

            var dbContext =
                webHost.Services.CreateScope().ServiceProvider.GetService<ApplicationDbContext>();

            var users = new List<UserEntity>()
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

            await dbContext.Users.AddRangeAsync(users);

            await dbContext.SaveChangesAsync();

            var httpClient = webHost.CreateClient();

            var username = "johndoe";

            // Act

            var response = await httpClient.GetAsync($"api/User/get-by-username/{username}");

            // Assert

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Create_WhenUserDoNotExists_ShouldReturnCreated()
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
                        options.UseInMemoryDatabase("UserCreateDb");
                    });
                });
            });

            var httpClient = webHost.CreateClient();

            var userCeateDto = new UserCreateDto()
            {
                FirstName = "Bod",
                LastName = "Doe",
                Username = "bobdoe",
                Email = "bobdoe@gmail.com"
            };

            var content = JsonContent.Create(userCeateDto);

            // Act

            var response = await httpClient.PostAsync("api/User/", content);

            // Assert

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task Update_WhenUserExists_ShouldReturnOk()
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
                        options.UseInMemoryDatabase("UserUpdatDb");
                    });
                });
            });

            var dbContext =
                webHost.Services.CreateScope().ServiceProvider.GetService<ApplicationDbContext>();

            var users = new List<UserEntity>()
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

            await dbContext.Users.AddRangeAsync(users);

            await dbContext.SaveChangesAsync();

            var httpClient = webHost.CreateClient();

            var userUpdateDto = new UserUpdateDto()
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Username = "johndoe",
                Email = "johndoe@gmail.com"
            };

            var content = JsonContent.Create(userUpdateDto);

            // Act

            var response = await httpClient.PutAsync("api/User/", content);

            // Assert

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Delete_WhenUserExists_ShouldReturnOk()
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
                        options.UseInMemoryDatabase("UserDeleteDb");
                    });
                });
            });

            var dbContext =
                webHost.Services.CreateScope().ServiceProvider.GetService<ApplicationDbContext>();

            var users = new List<UserEntity>()
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

            await dbContext.Users.AddRangeAsync(users);

            await dbContext.SaveChangesAsync();

            var httpClient = webHost.CreateClient();

            var id = 2;

            // Act

            var response = await httpClient.DeleteAsync($"api/User/{id}");

            // Assert

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
