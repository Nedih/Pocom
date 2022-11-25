using AutoMapper;
using Microsoft.AspNetCore.Hosting.Server;
using Pocom.BLL.Models;
using Pocom.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Pocom.BLL.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserAccount, UserDTO>().ReverseMap().ForMember(dest => dest.UserName, opt => opt.MapFrom(x => x.Email));
            CreateMap<Post, PostDTO>().ForMember(dest => dest.Author, opt => opt.MapFrom(x => x.Author.Email));
            CreateMap<Reaction, ReactionDTO>();
        }
    }
}
