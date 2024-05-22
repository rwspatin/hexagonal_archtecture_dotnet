using Microsoft.Extensions.DependencyInjection;
using PosterrPosts.Application.Contracts.Services;
using PosterrPosts.Application.Services;

namespace PosterrPosts.Application.IoC
{
    public static class ServicesIoC
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPostService, PostService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
    }
}
