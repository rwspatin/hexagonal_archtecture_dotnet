using AutoMapper;
using PosterrPosts.Application.Contracts.Services;
using PosterrPosts.Domain.DTOs;
using PosterrPosts.Domain.Entities;
using PosterrPosts.Infra.Contracts.Repositories;

namespace PosterrPosts.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        public UserService(
            IUserRepository userRepository, 
            IPostRepository postRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<UserDataDTO> GetUserById(int id)
        {
            var userEntity = await _userRepository.Get(id);
            if (userEntity == null)
                return null;

            var userDto = _mapper.Map<User, UserDTO>(userEntity);

            var postCount = await _postRepository.Count(x => x.UserId == id);

            return new UserDataDTO()
            {
                UserName = userDto.UserName,
                DateJoined = userDto.CreationDate.ToString("MMMM dd, yyyy"),
                QtdPosts = postCount
            };
        }
    }
}
