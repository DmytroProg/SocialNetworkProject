using AutoMapper;
using SocialNetwork.Core.DTOs;
using SocialNetwork.Core.Models;

namespace SocialNetwork.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Post, PostDTO>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id))
                .ReverseMap();
        }
    }
}
