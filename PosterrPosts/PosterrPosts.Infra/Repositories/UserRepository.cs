using PosterrPosts.Domain.Entities;
using PosterrPosts.Infra.Contracts.Repositories;

namespace PosterrPosts.Infra.Repositories
{
    internal class UserRepository : BaseRepository<User, int>, IUserRepository
    {
        public UserRepository(PosterrPostDbContext dbContext)
            : base(dbContext) { }
    }
}
