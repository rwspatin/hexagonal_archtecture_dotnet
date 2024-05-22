using PosterrPosts.Domain.Entities;
using PosterrPosts.Infra.Contracts.Repositories;

namespace PosterrPosts.Test.Mocks.Repositories
{
    internal class PostRepositoryMock : BaseRepositoryMock<Post, int>, IPostRepository
    {
    }
}
