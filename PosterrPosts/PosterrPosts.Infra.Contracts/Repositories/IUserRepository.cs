using PosterrPosts.Domain.Entities;

namespace PosterrPosts.Infra.Contracts.Repositories
{
    public interface IUserRepository : IBaseRepository<User, int>
    {
    }
}
