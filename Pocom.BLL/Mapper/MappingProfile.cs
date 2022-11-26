﻿using AutoMapper;
using Pocom.BLL.Models;
using Pocom.DAL.Entities;

namespace Pocom.BLL.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserAccount, UserDTO>().ReverseMap().ForMember(dest => dest.UserName, opt => opt.MapFrom(x => x.Email));
            CreateMap<Post, PostDTO>().ForMember(dest => dest.Author, opt => opt.MapFrom(x => x.Author.Email));
            CreateMap<Reaction, ReactionDTO>();
            CreateMap<ReactionDTO, Reaction>();
        }
    }
}
