using AutoMapper;
using Pocom.BLL.Models;
using Pocom.DAL.Entities;

namespace Pocom.BLL.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserAccount, UserDTO>().ReverseMap().ForMember(dest => dest.UserName, opt => opt.MapFrom(x => x.Email));
            CreateMap<Post, PostDTO>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(x => x.Author.Email))
                .ForMember(dest => dest.AuthorImage, opt => opt.MapFrom(x => x.Author.Image))
                .ForMember(dest => dest.ParentPostId, opt => opt.MapFrom(x => x.ParentPostId))
                .ForMember(dest => dest.CommentsCount, opt => opt.MapFrom((src, dest, _, context) => src.Comments?.Count))
                .ForMember(dest => dest.ReactionStats, opt => opt.MapFrom((src, dest, _, context) => PostDTO.GetReactionStats(src.Reactions)))
                .ForMember(dest => dest.UserReactionType, opt => opt.MapFrom((src, dest, _, context) => PostDTO.GetUserReactionType(src.Reactions, context.Items["userId"]?.ToString())));
            CreateMap<Reaction, ReactionDTO>().ForMember(dest => dest.ReactionType, opt => opt.MapFrom(x => x.Type));
            //CreateMap<ReactionDTO, Reaction>();
        }
    }
}
