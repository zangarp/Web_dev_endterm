using FinOpsAPI.Interfaces;
using FinOpsAPI.Repositories;
using FinOpsAPI.Services;

namespace FinOpsAPI.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<ISDFORepository, SDFORepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<AuthService>();
            services.AddScoped<SDFOService>();
            services.AddScoped<MenuService>();
            services.AddScoped<MessageService>();
            return services;
        }
    }
}
