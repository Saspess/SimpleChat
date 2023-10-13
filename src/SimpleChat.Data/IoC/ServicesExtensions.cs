using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleChat.Data.Contexts.Contracts;
using SimpleChat.Data.Contexts.Implementation;
using SimpleChat.Data.Repositories.Contracts;
using SimpleChat.Data.Repositories.Implementation;

namespace SimpleChat.Data.IoC
{
    public static class ServicesExtensions
    {
        public static IServiceCollection ConfigureDataLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .ConfigureSqlContext(configuration)
                .ConfigureDbContext()
                .ConfigureRepositories();

            return services;
        }

        public static IServiceCollection ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("sqlConnection")),
                ServiceLifetime.Transient);

            return services;
        }

        public static IServiceCollection ConfigureDbContext(this IServiceCollection services)
        {
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

            return services;
        }

        public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<IUserChatRepository, UserChatRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
