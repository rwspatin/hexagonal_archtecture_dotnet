using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PosterrPosts.Infra.Contracts.Repositories;
using PosterrPosts.Infra.Repositories;

namespace PosterrPosts.Infra.IoC
{
    public static class InfraIoC
    {
        public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PostConStr");

            services.AddDbContext<PosterrPostDbContext>(options =>
                options.UseNpgsql(connectionString)
            );

            services.AddScoped<DbContext, PosterrPostDbContext>();
            services.AddMemoryCache();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPostRepository, PostRepository>();

            services.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));

            

            return services;
        }
    }
}
