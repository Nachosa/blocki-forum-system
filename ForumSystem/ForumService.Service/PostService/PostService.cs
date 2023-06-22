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

namespace ForumSystem.Business
{
    public class PostService : IPostService
    {
        private readonly IPostRepository postRepo;

        public PostService(IPostRepository postRepo)
        {
            this.postRepo = postRepo;
        }

        public IList<Post> GetPosts(PostQueryParameters queryParams)
        {
            return this.postRepo.GetPosts(queryParams).ToList();
        }

        public Post CreatePost(Post post)
        {
            postRepo.CreatePost(post);
            return post;
        }

        public Post UpdatePostContent(int postId, Post newPost, string userName)
        {
            var currPost = postRepo.GetPostById(postId);
            //Проверката дали е админ трябва по-скоро да се прави от AuthManager - injection?
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

        public ICollection<Post> GetPostsWithTag(string tag1)
        {
            var tag = postRepo.GetTagWithName(tag1);
            if (tag is null) throw new EntityNotFoundException($"Tag with name:{tag1} was not found!");
            var posts = postRepo.GetPostsWithTag(tag1);
            if (posts is null || posts.Count == 0) throw new EntityNotFoundException($"Posts with tag:{tag1} were not found!");
            return posts;
        }
    }
}
