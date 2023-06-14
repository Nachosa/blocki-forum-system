using AutoMapper;
using ForumSystemDTO.PostDTO;
using ForumSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.CommentDTO;
using ForumSystemDTO.UserDTO;

namespace ForumSystem.Business.AutoMapperProfile
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateUserDTO, User>();
            CreateMap<UpdateUserDTO, User>();
            CreateMap<User, GetUserDTO>();


            CreateMap<CreatePostDto, Post>();
            CreateMap<Post, GetPostDto>()
                .ForMember(pDto => pDto.UserName, opt => opt.MapFrom(p => p.User.Username))
                .ForMember(pDto => pDto.LikesCount, opt => opt.MapFrom(p => p.Likes.Count));
            CreateMap<UpdatePostContentDto, Post>();

            CreateMap<CreateCommentDto, Comment>();
            CreateMap<Comment, GetCommentDto>();
            CreateMap<UpdateCommentContentDto, Comment>();
        }

    }
}
