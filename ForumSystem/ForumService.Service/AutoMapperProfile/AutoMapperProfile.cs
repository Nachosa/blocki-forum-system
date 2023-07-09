﻿using AutoMapper;
using ForumSystemDTO.PostDTO;
using ForumSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumSystemDTO.UserDTO;
using ForumSystemDTO.TagDTO;
using ForumSystemDTO.CommentDTO;
using ForumSystemDTO.ViewModels.PostViewModels;
using ForumSystemDTO.ViewModels.UserViewModels;
using ForumSystem.DataAccess.QueryParams;
using ForumSystemDTO.ViewModels.AdminModels;

namespace ForumSystem.Business.AutoMapperProfile
{
	public class AutoMapperProfile : Profile
	{


		public AutoMapperProfile()
		{
			CreateMap<CreateUserDTO, User>();
			CreateMap<UpdateUserDTO, User>();
			CreateMap<User, GetUserDTO>();

			CreateMap<User, EditUser>();
			CreateMap<EditUser, User>();
			CreateMap<RegisterUser, User>();

			CreateMap<SearchUser, UserQueryParams>()
		.ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.SearchOption == "FirstName" ? src.SearchOptionValue : null))
		.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.SearchOption == "Email" ? src.SearchOptionValue : null))
		.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.SearchOption == "UserName" ? src.SearchOptionValue : null));

			CreateMap<CreatePostDto, Post>();
			CreateMap<Post, GetPostDto>()
				.ForMember(pDto => pDto.UserName, opt => opt.MapFrom(p => p.User.Username))
				.ForMember(pDto => pDto.LikesCount, opt => opt.MapFrom(p => p.Likes.Where(l => l.IsDislike == false).Count()))
				.ForMember(pDto => pDto.DislikesCount, opt => opt.MapFrom(p => p.Likes.Where(l => l.IsDislike == true).Count()))
				.ForMember(pDto => pDto.Tags, opt => opt.MapFrom(p => !(p.Tags.Count <= 0) ? p.Tags.Select(pt => pt.Tag.Name).ToList() : null)) //Тук има нещо по-адекватно от null.
				.ForMember(pDto => pDto.Comments, opt => opt.MapFrom(p => !(p.Comments.Count <= 0) ? p.Comments.Select(c => c).ToList() : null));
			CreateMap<Post, GetPostDtoAbbreviated>()
				.ForMember(pDto => pDto.UserName, opt => opt.MapFrom(p => p.User.Username))
				.ForMember(pDto => pDto.LikesCount, opt => opt.MapFrom(p => p.Likes.Where(l => l.IsDislike == false).Count()))
				.ForMember(pDto => pDto.DislikesCount, opt => opt.MapFrom(p => p.Likes.Where(l => l.IsDislike == true).Count()))
				.ForMember(pDto => pDto.Tags, opt => opt.MapFrom(p => !(p.Tags.Count <= 0) ? p.Tags.Select(pt => pt.Tag.Name).ToList() : null)) //Тук има нещо по-адекватно от null.
				.ForMember(pDto => pDto.CommentsCount, opt => opt.MapFrom(p => p.Comments.Count));
			CreateMap<UpdatePostContentDto, Post>();
			CreateMap<CreatePostViewModel, Post>();
			CreateMap<EditPostViewModel, Post>();
			CreateMap<Post, EditPostViewModel>();
			CreateMap<Post, PostDetailsViewModel>()
				.ForMember(pDto => pDto.CreatedBy, opt => opt.MapFrom(p => p.User.Username))
				.ForMember(pDto => pDto.LikesCount, opt => opt.MapFrom(p => p.Likes.Where(l => l.IsDislike == false).Count()))
				.ForMember(pDto => pDto.DislikesCount, opt => opt.MapFrom(p => p.Likes.Where(l => l.IsDislike == true).Count()))
				.ForMember(pDto => pDto.Tags, opt => opt.MapFrom(p => !(p.Tags.Count <= 0) ? p.Tags.Select(pt => pt.Tag.Name).ToList() : null)) //Тук има нещо по-адекватно от null.
				.ForMember(pDto => pDto.Comments, opt => opt.MapFrom(p => !(p.Comments.Count <= 0) ? p.Comments.Select(c => c).ToList() : null));

			CreateMap<CreateCommentDto, Comment>();
			CreateMap<Comment, GetCommentDto>() //Така може да се направи GetCommentDto след преместване на мапването от сървиса в контролера.
				.ForMember(cDto => cDto.LikesCount, opt => opt.MapFrom(c => c.Likes.Count))
				.ForMember(cDto => cDto.UserName, opt => opt.MapFrom(c => c.User.Username));
			CreateMap<UpdateCommentContentDto, Comment>();

			CreateMap<TagDto, Tag>();
			CreateMap<Tag, TagDto>();
		}

	}
}
