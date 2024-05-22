using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using PosterrPosts.Application.Contracts.Services;
using PosterrPosts.Application.MapperProfiles;
using PosterrPosts.Application.Services;
using PosterrPosts.Infra.Contracts.Repositories;

namespace PosterrPosts.Test.Helper
{
    internal static class ServicesHelper
    {
        private static readonly IUserRepository userRepo = RepositoryHelper.CreateUserRepository();
        private static readonly IPostRepository postRepo = RepositoryHelper.CreatePostRepository();
        private static IMapper _mapper;
        
        private static void SetMapper()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new DefaultProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }

        internal static IUserService CreateUserService()
        {
            SetMapper();

            return new UserService(userRepo, postRepo, _mapper);
        }

        internal static IPostService CreatePostService()
        {
            SetMapper();

            var memoryCache = MemoryCacheHelper.GetMemoryCache();

            return new PostService(postRepo, memoryCache, _mapper);
        }
    }
}
