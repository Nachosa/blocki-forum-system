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

        public PostService(IPostRepository postRepo, IUserRepository userRepo, ITagRepository tagRepo)
        {
            this.postRepo = postRepo;
            this.userRepo = userRepo;
            this.tagRepo = tagRepo;
        }

		public List<Post> GetAllPosts()
        {
			return this.postRepo.GetAllPosts().ToList();
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
            if (like != null)
                throw new DuplicateEntityException("You can't like a post twice!");
            //Може би ще е по-добре да се подават само ИД-тата?
            postRepo.LikePost(post, user);
            return true;
        }

        public bool UnlikePost(int postId, string userName)
        {
            //В момента взимам поста и юзъра само за да проверя дали съществуват.
            var post = postRepo.GetPostById(postId);
            var user = userRepo.GetUserByUserName(userName);
            var like = postRepo.GetLike(postId, user.Id);
            if (like == null)
                throw new DuplicateEntityException("You haven't liked this post!");
            postRepo.UnikePost(like);
            return true;
        }

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
            if (currPost.User.Username != userName)
                throw new UnauthenticatedOperationException("Can't update other user's posts!");
            else
                return postRepo.UpdatePostContent(newPost, currPost);
        }

        public bool DeletePostById(int postId, string userName)
        {
            var post = postRepo.GetPostById(postId);
            if (post.User.Username != userName)
                throw new UnauthenticatedOperationException("Can't delete other user's posts!");
            return postRepo.DeletePostById(postId);
        }

        public Post GetPostById(int postId)
        {
            return postRepo.GetPostById(postId);
        }

        //public ICollection<Post> GetPostsWithTag(string tag1)
        //{
        //    var tag = postRepo.GetTagWithName(tag1);
        //    if (tag is null) throw new EntityNotFoundException($"Tag with name:{tag1} was not found!");
        //    var posts = postRepo.GetPostsWithTag(tag1);
        //    if (posts is null || posts.Count == 0) throw new EntityNotFoundException($"Posts with tag:{tag1} were not found!");
        //    return posts;
        //}
    }
}
