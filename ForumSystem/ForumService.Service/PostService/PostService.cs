using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumSystem.DataAccess.Models;
using ForumSystem.DataAccess;
using ForumSystem.DataAccess.ReposContracts;
using ForumSystem.DataAccess.Dtos;
using AutoMapper;

namespace ForumSystem.Business
{
    public class PostService : IPostService
    {
        private readonly IPostRepository repo;
        private readonly IMapper postMapper;

        public PostService(IPostRepository repo, IMapper postMapper)
        {
            this.repo = repo;
            this.postMapper = postMapper;
        }

        public IList<GetPostDto> GetPosts()
        {
            IList<Post> posts = this.repo.GetPosts().ToList();
            return posts.Select(post => postMapper.Map<GetPostDto>(post)).ToList();
        }

        public Post CreatePost(CreatePostDto postDto)
        {
            Post post = postMapper.Map<Post>(postDto);

            post.Id = Post.Count;
            Post.Count += 1;

            repo.CreatePost(post);
            return post;
        }

        public Post UpdatePostContent(int postId, UpdatePostContentDto postContentDto)
        {
            return repo.UpdatePostContent(postId, postContentDto);
        }

        public Post DeletePostById(int postId)
        {
            return repo.DeletePostById(postId);
        }

        public GetPostDto GetPostById(int postId)
        {
            return postMapper.Map<GetPostDto>(repo.GetPostById(postId));
        }
    }
}
