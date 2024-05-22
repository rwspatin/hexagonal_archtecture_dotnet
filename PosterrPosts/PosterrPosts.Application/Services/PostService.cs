using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using PosterrPosts.Application.Contracts.Services;
using PosterrPosts.Application.Helper;
using PosterrPosts.Domain.DTOs;
using PosterrPosts.Domain.Entities;
using PosterrPosts.Domain.Enum;
using PosterrPosts.Infra.Contracts.Repositories;

namespace PosterrPosts.Application.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMemoryCache _memoryCache;
        private readonly IMapper _mapper;

        public PostService(
            IPostRepository postRepository,
            IMemoryCache memoryCache,
            IMapper mapper)
        {
            _postRepository = postRepository;
            _memoryCache = memoryCache;
            _mapper = mapper;
        }

        public async Task<List<PostDTO>> GetAllPosts(DateTime? from = null, DateTime? to = null)
        {
            var postKey = PostCacheHelper.GetKeyByParameters(from, to);

            if (!_memoryCache.TryGetValue(postKey, out List<PostDTO> post))
            {
                IEnumerable<Post> posts;
                if(from.HasValue || to.HasValue)
                    posts = await _postRepository.GetAll(x => (from.HasValue ? x.CreateDate.Date >= from.Value.Date : true)
                                    && (to.HasValue ? x.CreateDate.Date <= to.Value.Date : true));
                else
                    posts = await _postRepository.GetAll();

                post = _mapper.Map<List<Post>, List<PostDTO>>(posts.ToList());

                _memoryCache.Set(postKey, post);
            }

            return post;
        }

        public async Task<List<PostDTO>> GetAllPostsByUser(int userId, DateTime? from = null, DateTime? to = null)
        {
            var postKey = PostCacheHelper.GetKeyByParameters(from, to);
            var userKey = $"{postKey}_{userId}";

            if (!_memoryCache.TryGetValue(userKey, out IEnumerable<PostDTO> post))
            {
                var posts = await _postRepository.GetAll(x => x.UserId == userId 
                                    && (from.HasValue ? x.CreateDate.Date >= from.Value.Date : true)
                                    && (to.HasValue ? x.CreateDate.Date <= to.Value.Date : true));
                post = _mapper.Map<IEnumerable<Post>, IEnumerable<PostDTO>>(posts);

                _memoryCache.Set(userKey, post, TimeSpan.FromMinutes(10));
            }

            return post.OrderBy(x => x.CreateDate).ToList();
        }

        public async Task<(bool, string)> AddPost(CreatePostDTO postDTO)
        {
            if (postDTO.PostText.Length > MAX_LENGHT)
                return (false, MAX_LENGHT_POST);

            var qtdPosts = await CountUserPostsToday(postDTO.UserId);
            if (qtdPosts >= LIMIT_POSTS)
                return (false, MAX_POSTS_TODAY);

            var postEntity = _mapper.Map<CreatePostDTO, Post>(postDTO);

            postEntity.PostType = EPostType.POST;
            postEntity.CreateDate = DateTime.UtcNow;

            await _postRepository.Add(postEntity);

            _postRepository.SaveChanges();

            await UpdateCache();

            return (true, null);
        }

        private const int LIMIT_POSTS = 5;
        private const int MAX_LENGHT = 777;
        private const string MAX_POSTS_TODAY = "Max day alowed posts of user for today";
        private const string MAX_LENGHT_POST = "Post must have 777 chars or less";
        public async Task<(bool, string)> ReactToPost(CreatePostDTO postDTO, int postResponseId, EPostType postType)
        {
            if (postDTO.PostText.Length > MAX_LENGHT)
                return (false, MAX_LENGHT_POST);

            var qtdPosts = await CountUserPostsToday(postDTO.UserId);
            if (qtdPosts >= LIMIT_POSTS)
                return (false, MAX_POSTS_TODAY);

            var post = await _postRepository.Get(postResponseId);
            if (post.UserId == postDTO.UserId)
                return (false, "User can not react to their own posts");
            
            if (postType == EPostType.QUOTE && post.PostType == EPostType.QUOTE)
                return (false, "Not alowed to quote a quote post");
            else if (postType == EPostType.REPOSTE && post.PostType == EPostType.REPOSTE)
                return (false, "Not alowed to repost a repost post");

            var postEntity = _mapper.Map<CreatePostDTO, Post>(postDTO);

            postEntity.PostType = postType;
            postEntity.PostId = postResponseId;
            postEntity.CreateDate = DateTime.UtcNow;

            await _postRepository.Add(postEntity);

            _postRepository.SaveChanges();

            await UpdateCache();

            return (true, null);
        }

        private async Task UpdateCache()
        {
            var posts = await _postRepository.GetAll();
            var post = _mapper.Map<List<Post>, List<PostDTO>>(posts.ToList());

            _memoryCache.Set(PostCacheHelper.GetKeyByParameters(), post);
        }

        public async Task<int> CountUserPostsToday(int userId)
            => await _postRepository.Count(x => x.UserId == userId && x.CreateDate == DateTime.Now.Date);
    }
}
