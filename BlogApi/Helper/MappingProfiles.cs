using AutoMapper;
using BlogApi.Dto;
using BlogApi.Models;

namespace BlogApi.Helper
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<Post, PostDto>();
            CreateMap<PostDto, Post>();
        }

    }
}
