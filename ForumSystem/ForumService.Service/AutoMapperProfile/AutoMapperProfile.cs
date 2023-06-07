using AutoMapper;
using ForumSystem.Business.CreateAndUpdate_UserDTO;
using ForumSystem.DataAccess.Dtos;
using ForumSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.Business.AutoMapperProfile
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateUserDTO, User>();
            CreateMap<UpdateUserDTO, User>();
            CreateMap<CreatePostDto, Post>();
            CreateMap<GetPostDto, Post>();
            CreateMap<UpdatePostContentDto, Post>();
        }

    }
}
