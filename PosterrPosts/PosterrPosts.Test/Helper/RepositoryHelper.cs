using PosterrPosts.Infra.Contracts.Repositories;
using PosterrPosts.Test.Mocks.Repositories;

namespace PosterrPosts.Test.Helper
{
    internal static class RepositoryHelper
    {
        internal static IUserRepository CreateUserRepository()
        {
            return new UserRepositoryMock();
        }

        internal static IPostRepository CreatePostRepository()
        {
            return new PostRepositoryMock();
        }
    }
}
