using PosterrPosts.Application.Contracts.Services;
using PosterrPosts.Domain.DTOs;
using PosterrPosts.Test.Helper;
using System;
using System.Linq;
using Xunit;

namespace PosterrPosts.Test.Services
{
    public class PostServiceTest
    {
        private readonly IPostService _postService = ServicesHelper.CreatePostService();
        private static readonly string date = DateTime.Now.Date.ToString("MMMM dd, yyyy");

        [Fact]
        public void MustReturnAllPosts()
        {
            var posts = _postService.GetAllPosts().Result;
            
            Assert.True(posts.Count > 1);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void MustReturnUserPosts(int idUser)
        {
            var posts = _postService.GetAllPostsByUser(idUser).Result;

            Assert.True(posts.Count >= idUser);
        }

        [Fact]
        public void MustHaveUserTodayPostsQtd()
        {
            var currentUserQtd = _postService.CountUserPostsToday(1).Result;

            Assert.True(currentUserQtd >= 1);
        }

        [Fact]
        public void MustNotAcceptQuoteAQuotePosts()
        {
            _postService.ReactToPost(new CreatePostDTO()
            {
                UserId = 2,
                PostText = "Other post"
            }, 10, Domain.Enum.EPostType.QUOTE).GetAwaiter().GetResult();

            var all = _postService.GetAllPosts().Result;
            var quotePost = all.FirstOrDefault(x => x.PostType == Domain.Enum.EPostType.QUOTE
                                            && x.User.Id != 1);

            var (sucess, erro) = _postService.ReactToPost(new CreatePostDTO()
            {
                UserId = 1,
                PostText = "Other post"
            }, quotePost?.Id ?? 1, Domain.Enum.EPostType.QUOTE).GetAwaiter().GetResult();

            Assert.False(sucess);
            Assert.Contains("Not alowed to quote a quote post", erro);
        }

        [Fact]
        public void MustNotAcceptResponseAResponsePosts()
        {
            _postService.ReactToPost(new CreatePostDTO()
            {
                UserId = 3,
                PostText = "Other post"
            }, 2, Domain.Enum.EPostType.REPOSTE).GetAwaiter().GetResult();

            var all = _postService.GetAllPosts().Result;
            var quotePost = all.FirstOrDefault(x => x.PostType == Domain.Enum.EPostType.REPOSTE
                                            && x.User.Id != 1);

            var (sucess, erro) = _postService.ReactToPost(new CreatePostDTO()
            {
                UserId = 1,
                PostText = "Other post"
            }, quotePost?.Id ?? 1, Domain.Enum.EPostType.REPOSTE).GetAwaiter().GetResult();

            Assert.False(sucess);
            Assert.Contains("Not alowed to repost a repost post", erro);
        }

        [Fact]
        public void MustNotAcceptPostsMoreThanFivePosts()
        {
            var (sucess, erro) = _postService.AddPost(new CreatePostDTO()
            {
                UserId = 5,
                PostText = "Other post"
            }).GetAwaiter().GetResult();

            Assert.False(sucess);
            Assert.Contains("Max day alowed posts of user for today", erro);
        }

        [Fact]
        public void MustNotAcceptPostsMoreThanFivePostsOnReact()
        {
            var (sucess, erro) = _postService.ReactToPost(new CreatePostDTO()
            {
                UserId = 5,
                PostText = "Other post"
            }, 1, Domain.Enum.EPostType.REPOSTE).GetAwaiter().GetResult();

            Assert.False(sucess);
            Assert.Contains("Max day alowed posts of user for today", erro);
        }

        [Fact]
        public void MustNotAcceptUserReactToOwnPosts()
        {
            var (sucess, erro) = _postService.ReactToPost(new CreatePostDTO()
            {
                UserId = 1,
                PostText = "Other post"
            }, 1, Domain.Enum.EPostType.REPOSTE).GetAwaiter().GetResult();

            Assert.False(sucess);
            Assert.Contains("User can not react to their own posts", erro);
        }

        private const string GIANT_POST = "QqnrlcYwrP4rQK1c9JY8Tt77XoKaVsgOl38gk7dD3UAa5qjUZXLZ1v8txuCFnVqHCONxBRSHuZlnqAvdtU1vIYbu8OnPJI8HIh6wgtHsJyFbVHzWU3p7PEM2y2DByXv7CBY87taQ5CJEdWwHFEGF4Ue7eBqnvO5kG5oIKz29biYO9J4rt3euGir6WbonSbbFzic81p0p9pa8FXGrmxSQdEh4Rl8DKvk7We1NLn65yYUXgYTfjwEX2W6wKyGkYkZv1MlYXhV9J8axvVQ6J5o3I3el6CA6Qma96ObVksRE0SQvnWvfWczGEsBlt7GPvvwZV57gRllyyOZHut9Qa8EVJUvfA5HoIgNEeJeZCHhxmxRp2ivO1wRragYP4VuFHydMqojSjR67kGx61LJzJhd3kDNrRGwBbUcAE6Hgmuf0LkgyrtsfSFUqR6a4eN2PpVTQiSTfZv9u60NWlLpuQuzdXkjP2n8aTpHAUNtDUwTxUziS5psrwzHXIUSQIPW3f9KpiXcOimg0eytA9QwjIe9EW9EzRk1lueq9gpRuIg9VHfmiowFAhapVYpgfBrS0LlTXSMkVxnPwYqCecKrEOsrxle25KsmWc72oM6Fcw0lGBXfRGKRVuKOkrfVnaU6fi0ov7Sqh5ZIkMgvexHEWZdbNDPGfJvmi1FDhECWiMUaY5ArKidzA7n4DbSZ4OVaTCQfeegqFvf5qPEL5J8J8EstGVSFn6zf1KALKwO1OGhjspWy6n58X333ElFNJvCB1gaxBSfhCjkoo88UNBrn59flvB8QK7h01jITy";

        [Fact]
        public void MustNotAcceptAddPostsWithMoreThan777Chars()
        {
            var (sucess, erro) = _postService.ReactToPost(new CreatePostDTO()
            {
                UserId = 3,
                PostText = GIANT_POST
            }, 1, Domain.Enum.EPostType.REPOSTE).GetAwaiter().GetResult();

            Assert.False(sucess);
            Assert.Contains("Post must have 777 chars or less", erro);
        }

        [Fact]
        public void MustNotAcceptAddReactPostsWithMoreThan777Chars()
        {
            var (sucess, erro) = _postService.ReactToPost(new CreatePostDTO()
            {
                UserId = 3,
                PostText = GIANT_POST
            }, 1, Domain.Enum.EPostType.REPOSTE).GetAwaiter().GetResult();

            Assert.False(sucess);
            Assert.Contains("Post must have 777 chars or less", erro);
        }
    }
}
