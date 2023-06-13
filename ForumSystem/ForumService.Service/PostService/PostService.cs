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

        public Post UpdatePostContent(int postId, Post post, string userName)
        {
            var postUser = postRepo.GetPostById(postId).User.Username;
            if (postUser != userName)
            {
                throw new ArgumentException("Can't update other user's posts!");
            }
            return postRepo.UpdatePostContent(postId, post, userName);
        }

        public bool DeletePostById(int postId)
        {
            return postRepo.DeletePostById(postId);
        }

        public Post GetPostById(int postId)
        {
            return postRepo.GetPostById(postId);
        }
    }
}
