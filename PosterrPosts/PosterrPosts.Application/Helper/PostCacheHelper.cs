namespace PosterrPosts.Application.Helper
{
    public static class PostCacheHelper
    {
        const string POST_KEY = "ALL_POSTS";
        public static string GetKeyByParameters(DateTime? from = null, DateTime? to = null)
        {
            if (from == null && to == null)
                return POST_KEY;

            var postKey = POST_KEY;

            if (from != null)
                postKey += $"_from_{from.Value:dd-MM-yyyy}";

            if (to != null)
                postKey += $"_to_{from.Value:dd-MM-yyyy}";

            return postKey;
        }
    }
}
