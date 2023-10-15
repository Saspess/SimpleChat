using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SimpleChat.Business.Services.Contracts;
using SimpleChat.Business.Services.Implementation;
using System.Reflection;

namespace SimpleChat.Business.IoC
{
    public static class ServicesExtensions
    {
        public static IServiceCollection ConfigureBusinessLayer(this IServiceCollection services)
        {
            services
                .ConfigureAutoMapper()
                .ConfigureServices()
                .ConfigureFluentValidation();

            return services;
        }

        public static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }

        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IUserChatService, UserChatService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }

        public static IServiceCollection ConfigureFluentValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
