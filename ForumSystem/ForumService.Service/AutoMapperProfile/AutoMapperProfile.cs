using AutoMapper;
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
using ForumSystemDTO.ViewModels.CommentViewModels;
using ForumSystem.Api.QueryParams;

namespace ForumSystem.Business.AutoMapperProfile
{
	public class AutoMapperProfile : Profile
	{


		public AutoMapperProfile()
		{
			CreateMap<SearchUser, UserQueryParams>()
		.ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.SearchOption == "FirstName" ? src.SearchOptionValue : null))
		.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.SearchOption == "Email" ? src.SearchOptionValue : null))
		.ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.SearchOption == "Username" ? src.SearchOptionValue : null));
			CreateMap<FilterPosts, PostQueryParameters>();

			CreateMap<CreatePostDto, Post>();
			CreateMap<Post, GetPostDto>()
				.ForMember(pDto => pDto.Username, opt => opt.MapFrom(p => p.User.Username))
				.ForMember(pDto => pDto.LikesCount, opt => opt.MapFrom(p => p.Likes.Where(l => l.IsDislike == false).Count()))
				.ForMember(pDto => pDto.DislikesCount, opt => opt.MapFrom(p => p.Likes.Where(l => l.IsDislike == true).Count()))
				.ForMember(pDto => pDto.Tags, opt => opt.MapFrom(p => !(p.Tags.Count <= 0) ? p.Tags.Select(pt => pt.Tag.Name).ToList() : null)) //Тук има нещо по-адекватно от null.
				.ForMember(pDto => pDto.Comments, opt => opt.MapFrom(p => !(p.Comments.Count <= 0) ? p.Comments.Select(c => c).ToList() : null));
			CreateMap<Post, GetPostDtoAbbreviated>()
				.ForMember(pDto => pDto.Username, opt => opt.MapFrom(p => p.User.Username))
				.ForMember(pDto => pDto.LikesCount, opt => opt.MapFrom(p => p.Likes.Where(l => l.IsDislike == false).Count()))
				.ForMember(pDto => pDto.DislikesCount, opt => opt.MapFrom(p => p.Likes.Where(l => l.IsDislike == true).Count()))
				.ForMember(pDto => pDto.Tags, opt => opt.MapFrom(p => !(p.Tags.Count <= 0) ? p.Tags.Select(pt => pt.Tag.Name).ToList() : null)) //Тук има нещо по-адекватно от null.
				.ForMember(pDto => pDto.CommentsCount, opt => opt.MapFrom(p => p.Comments.Count));
			CreateMap<UpdatePostContentDto, Post>();
			CreateMap<CreatePostViewModel, Post>();
			CreateMap<EditPostViewModel, Post>();
			CreateMap<Post, EditPostViewModel>();
			CreateMap<Post, PostDetailsViewModel>()
				.ForMember(pVM => pVM.CreatedBy, opt => opt.MapFrom(p => p.User.Username))
				.ForMember(pVM => pVM.LikesCount, opt => opt.MapFrom(p => p.Likes.Where(l => l.IsDislike == false).Count()))
				.ForMember(pVM => pVM.DislikesCount, opt => opt.MapFrom(p => p.Likes.Where(l => l.IsDislike == true).Count()))
				.ForMember(pVM => pVM.Tags, opt => opt.MapFrom(p => !(p.Tags.Count <= 0) ? p.Tags.Select(pt => pt.Tag.Name).ToList() : null)) //Тук има нещо по-адекватно от null.
				.ForMember(pVM => pVM.Comments, opt => opt.MapFrom(p => !(p.Comments.Count <= 0) ? p.Comments.Select(c => c).ToList() : null));
			CreateMap<Post, PostViewModelAbbreviated>()
				.ForMember(pVM => pVM.CreatedBy, opt => opt.MapFrom(p => p.User.Username))
				.ForMember(pVM => pVM.LikesCount, opt => opt.MapFrom(p => p.Likes.Where(l => l.IsDislike == false).Count()))
				.ForMember(pVM => pVM.DislikesCount, opt => opt.MapFrom(p => p.Likes.Where(l => l.IsDislike == true).Count()))
				.ForMember(pVM => pVM.Tags, opt => opt.MapFrom(p => !(p.Tags.Count <= 0) ? p.Tags.Select(pt => pt.Tag.Name).ToList() : null)) //Тук има нещо по-адекватно от null.
				.ForMember(pVM => pVM.CommentsCount, opt => opt.MapFrom(p => p.Comments.Count));

			CreateMap<CreateCommentDto, Comment>();
			CreateMap<Comment, GetCommentDto>()
				.ForMember(cDto => cDto.LikesCount, opt => opt.MapFrom(c => c.Likes.Count))
				.ForMember(cDto => cDto.Username, opt => opt.MapFrom(c => c.User.Username));
			CreateMap<UpdateCommentContentDto, Comment>();
			CreateMap<Comment, CommentViewModel>();
			//.ForMember(cDto => cDto.LikesCount, opt => opt.MapFrom(c => c.Likes.Count))

			CreateMap<CreateUserDTO, User>();
			CreateMap<UpdateUserDTO, User>();
			CreateMap<User, GetUserDTO>();
			CreateMap<User, EditUser>();
			CreateMap<EditUser, User>();
			CreateMap<RegisterUser, User>();
			CreateMap<User, UserDetailsViewModel>()
				.ForMember(uVM => uVM.LikesCount, opt => opt.MapFrom(u => u.Posts.Sum(p => p.Likes.Where(l => !l.IsDislike).Count())))
				.ForMember(uVM => uVM.DislikesCount, opt => opt.MapFrom(u => u.Posts.Sum(p => p.Likes.Where(l => l.IsDislike).Count())))
				.ForMember(uVM => uVM.Comments, opt => opt.MapFrom(u => !(u.Comments.Count <= 0) ? u.Comments.Select(c => c).ToList() : null))
				.ForMember(uVM => uVM.Posts, opt => opt.MapFrom(u => !(u.Posts.Count <= 0) ? u.Posts.Select(p => p).ToList() : null));

			CreateMap<TagDto, Tag>();
			CreateMap<Tag, TagDto>();
		}

	}
}
