using PosterrPosts.Domain.DTOs;

namespace PosterrPosts.Application.Contracts.Services
{
    public interface IUserService
    {
        Task<UserDataDTO> GetUserById(int id);
    }
}
