using PosterrPosts.Domain.DTOs;
using PosterrPosts.Domain.Enum;

namespace PosterrPosts.Application.Contracts.Services
{
    public interface IPostService
    {
        Task<List<PostDTO>> GetAllPosts(DateTime? from = null, DateTime? to = null);
        Task<List<PostDTO>> GetAllPostsByUser(int userId, DateTime? from = null, DateTime? to = null);
        Task<(bool, string)> AddPost(CreatePostDTO postDTO);
        Task<(bool, string)> ReactToPost(CreatePostDTO postDTO, int postResponseId, EPostType postType);
        Task<int> CountUserPostsToday(int userId);
    }
}
