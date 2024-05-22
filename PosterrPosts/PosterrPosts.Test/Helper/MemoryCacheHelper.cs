using Microsoft.Extensions.Caching.Memory;
using Moq;

namespace PosterrPosts.Test.Helper
{
    internal static class MemoryCacheHelper
    {
        public static IMemoryCache GetMemoryCache()
        {
            var mockMemoryCache = new Mock<IMemoryCache>();
            var cachEntry = Mock.Of<ICacheEntry>();
            mockMemoryCache
                .Setup(m => m.CreateEntry(It.IsAny<object>()))
                .Returns(cachEntry);

            return mockMemoryCache.Object;
        }
    }
}
