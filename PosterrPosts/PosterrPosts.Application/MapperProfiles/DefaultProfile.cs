using AutoMapper;
using PosterrPosts.Domain.DTOs;
using PosterrPosts.Domain.Entities;

namespace PosterrPosts.Application.MapperProfiles
{
    public class DefaultProfile : Profile
    {
        public DefaultProfile()
        {
            CreateMap<Post, PostDTO>()
                .ReverseMap();

            CreateMap<Post, CreatePostDTO>()
                .ForMember(dest => dest.PostText, map => map.MapFrom(x => x.PostText))
                .ForMember(dest => dest.UserId, map => map.MapFrom(x => x.UserId))
                .ReverseMap();

            CreateMap<User, UserDTO>()
                .ReverseMap();
        }
    }
}
