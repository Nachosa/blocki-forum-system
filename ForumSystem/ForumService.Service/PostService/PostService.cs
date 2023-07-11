using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumSystem.DataAccess.Models;
using ForumSystem.DataAccess;
using ForumSystemDTO.PostDTO;
using AutoMapper;
using ForumSystem.DataAccess.PostRepo;
using ForumSystem.Api.QueryParams;
using ForumSystem.DataAccess.Exceptions;
using ForumSystem.Business.AuthenticationManager;
using ForumSystem.DataAccess.UserRepo;
using ForumSystem.DataAccess.TagRepo;

namespace ForumSystem.Business
{
    public class PostService : IPostService
    {
        private readonly IPostRepository postRepo;
        private readonly IUserRepository userRepo;
        private readonly ITagRepository tagRepo;
        private readonly IAuthManager authManager;

        public PostService(IPostRepository postRepo, IUserRepository userRepo, ITagRepository tagRepo, IAuthManager authManager)
        {
            this.postRepo = postRepo;
            this.userRepo = userRepo;
            this.tagRepo = tagRepo;
            this.authManager = authManager;
        }

		public List<Post> GetPosts(PostQueryParameters queryParams)
        {
            return this.postRepo.GetPosts(queryParams).ToList();
        }
        public int GetPostsCount()
        {
            return postRepo.GetPostsCount();
             
        }
        public Post CreatePost(Post post, string userName)
        {
            var user = userRepo.GetUserByUserName(userName);
            post.UserId = user.Id;
            postRepo.CreatePost(post);
            return post;
        }

        public bool LikePost(int postId, string userName)
        {
            //В момента взимам поста и юзъра само за да проверя дали съществуват.
            var post = postRepo.GetPostById(postId);
            var user = userRepo.GetUserByUserName(userName);
            var like = postRepo.GetLike(postId, user.Id);
            if (like is null)
            {
				postRepo.CreateLike(post, user);
			}
            else if (like is not null & (like.IsDeleted || like.IsDislike))
            {
				postRepo.LikePost(like);
			}
			else //Може би да е else if?
			{
                postRepo.DeleteLike(like);
			}
			//throw new DuplicateEntityException("You can't like a post twice!");
			//Може би ще е по-добре да се подават само ИД-тата?
            return true;
        }

		public bool DislikePost(int postId, string userName)
		{
			//В момента взимам поста и юзъра само за да проверя дали съществуват.
			var post = postRepo.GetPostById(postId);
			var user = userRepo.GetUserByUserName(userName);
			var like = postRepo.GetLike(postId, user.Id);
			if (like is null)
			{
				postRepo.CreateLike(post, user);
			}
			else if (like is not null & (like.IsDeleted || !like.IsDislike))
			{
				postRepo.DislikePost(like);
			}
			else //Може би да е else if?
			{
				postRepo.DeleteLike(like);
			}
			//throw new DuplicateEntityException("You can't like a post twice!");
			//Може би ще е по-добре да се подават само ИД-тата?
			return true;
		}

		//public bool UnlikePost(int postId, string userName)
  //      {
  //          //В момента взимам поста и юзъра само за да проверя дали съществуват.
  //          var post = postRepo.GetPostById(postId);
  //          var user = userRepo.GetUserByUserName(userName);
  //          var like = postRepo.GetLike(postId, user.Id);
  //          if (like == null)
  //              throw new DuplicateEntityException("You haven't liked this post!");
  //          postRepo.UnikePost(like);
  //          return true;
  //      }

        public bool TagPost(int postId, string userName, Tag tag)
        {
            //В момента взимам поста и юзъра само за да проверя дали съществуват.
            var currPost = postRepo.GetPostById(postId);
            var user = userRepo.GetUserByUserName(userName);
            var existingTag = tagRepo.GetTagByName(tag.Name);
            if (currPost.User.Username != userName)
                throw new UnauthenticatedOperationException("Can't tag other user's posts!");
            if (existingTag == null)
                tag = tagRepo.CreateTag(tag);
            else if (existingTag.Posts.Any(p => p.PostId == postId))
                throw new DuplicateEntityException("The post already has that tag!");
            postRepo.TagPost(currPost, tag);
            return true;
        }

        public Post UpdatePostContent(int postId, Post newPost, string userName)
        {
            var currPost = postRepo.GetPostById(postId);
            var loggedUser = userRepo.GetUserByUserName(userName);
            if (!authManager.AdminCheck(loggedUser) && currPost.User.Username != userName)
                throw new UnauthenticatedOperationException("Can't update other user's posts!");
            else
                return postRepo.UpdatePostContent(newPost, currPost);
        }

        public bool DeletePostById(int postId, string userName)
        {
            var post = postRepo.GetPostById(postId);
            var loggedUser = userRepo.GetUserByUserName(userName);
            if (!authManager.AdminCheck(loggedUser) && post.User.Username != userName)
                throw new UnauthenticatedOperationException("Can't delete other user's posts!");
            return postRepo.DeletePostById(postId);
        }

        public Post GetPostById(int postId)
        {
            return postRepo.GetPostById(postId);
        }
    }
}
