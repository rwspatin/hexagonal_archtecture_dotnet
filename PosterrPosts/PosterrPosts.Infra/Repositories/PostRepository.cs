using PosterrPosts.Domain.Entities;
using PosterrPosts.Infra.Contracts.Repositories;

namespace PosterrPosts.Infra.Repositories
{
    internal class PostRepository : BaseRepository<Post, int>, IPostRepository
    {
        public PostRepository(PosterrPostDbContext dbContext)
            : base(dbContext) { }
    }
}
