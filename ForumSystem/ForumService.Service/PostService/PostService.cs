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
        public ICollection<Post> GetPostsWithTag(string tag1)
        {
            var tag = postRepo.GetTagWithName(tag1);
            if (tag is null) throw new EntityNotFoundException("Tag with name:{tag1} was not found!");
            var posts = postRepo.GetPostsWithTag(tag1);
            if (posts is null || posts.Count == 0) throw new EntityNotFoundException("Posts with tag:{tag1} were not found!");
            return posts;
        }
    }
}
