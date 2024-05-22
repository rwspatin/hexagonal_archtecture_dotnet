using PosterrPosts.Domain.Entities;
using PosterrPosts.Infra.Contracts.Repositories;

namespace PosterrPosts.Test.Mocks.Repositories
{
    internal class UserRepositoryMock : BaseRepositoryMock<User, int>, IUserRepository
    {
    }
}
