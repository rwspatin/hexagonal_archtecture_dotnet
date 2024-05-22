using PosterrPosts.Domain.Entities;
using PosterrPosts.Domain.Enum;
using PosterrPosts.Infra.Contracts.Repositories;
using PosterrPosts.Test.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PosterrPosts.Test.Repositories
{
    public class PostRepositoryTest
    {
        private static IPostRepository repository = RepositoryHelper.CreatePostRepository();
        
        [Fact]
        public void MustInsertPost()
        {
            repository.Add(new Post()
            {
                PostText = "Teste",
                PostType = EPostType.POST,
                CreateDate = DateTime.Now.Date,
                UserId = 1
            }).GetAwaiter().GetResult();
            
            repository.SaveChanges();
        }

        [Fact]
        public void MustGetThePost()
        {
            repository.Add(new Post()
            {
                PostText = "Teste",
                PostType = EPostType.POST,
                CreateDate = DateTime.Now.Date,
                UserId = 1
            }).GetAwaiter().GetResult();

            repository.SaveChanges();

            var post = repository.Get(1).Result;

            Assert.NotNull(post);
        }

        [Fact]
        public void MustGetAllPost()
        {
            repository.Add(new Post()
            {
                PostText = "Teste",
                PostType = EPostType.POST,
                CreateDate = DateTime.Now.Date,
                UserId = 1
            }).GetAwaiter().GetResult();

            repository.Add(new Post()
            {
                PostText = "Teste",
                PostType = EPostType.QUOTE,
                CreateDate = DateTime.Now.Date,
                UserId = 3
            }).GetAwaiter().GetResult();

            repository.Add(new Post()
            {
                PostText = "Teste",
                PostType = EPostType.REPOSTE,
                CreateDate = DateTime.Now.Date,
                UserId = 2
            }).GetAwaiter().GetResult();

            repository.SaveChanges();

            var post = repository.GetAll().Result;

            Assert.NotNull(post.FirstOrDefault(x => x.UserId == 1));
            Assert.NotNull(post.FirstOrDefault(x => x.UserId == 2));
            Assert.NotNull(post.FirstOrDefault(x => x.UserId == 3));
        }
    }
}
