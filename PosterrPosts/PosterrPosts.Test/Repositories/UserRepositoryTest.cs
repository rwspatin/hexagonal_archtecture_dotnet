using PosterrPosts.Test.Helper;
using System;
using System.Linq;
using Xunit;

namespace PosterrPosts.Test.Repositories
{
    public class UserRepositoryTest
    {
        [Fact]
        public void MustGetUserData()
        {
            var repo = RepositoryHelper.CreateUserRepository();

            var user = repo.Get(1).Result;

            Assert.NotNull(user);
        }

        [Fact]
        public void MustGetAllUser()
        {
            var repo = RepositoryHelper.CreateUserRepository();

            var users = repo.GetAll().Result;

            Assert.Equal(5, users.Count());
        }

        [Fact]
        public void MustCountUsersCreatedToday()
        {
            var repo = RepositoryHelper.CreateUserRepository();
            var today = DateTime.Now.Date;

            var users = repo.Count(x => x.CreationDate == today).Result;

            Assert.Equal(5, users);
        }
    }
}
