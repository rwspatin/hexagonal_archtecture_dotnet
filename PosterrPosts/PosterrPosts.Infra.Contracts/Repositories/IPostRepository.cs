using PosterrPosts.Domain.Entities;

namespace PosterrPosts.Infra.Contracts.Repositories
{
    public interface IPostRepository : IBaseRepository<Post, int>
    {
    }
}
