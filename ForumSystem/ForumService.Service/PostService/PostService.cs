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
        private readonly IPostRepository repo;
        private readonly IMapper postMapper;

        public PostService(IPostRepository repo, IMapper postMapper)
        {
            this.repo = repo;
            this.postMapper = postMapper;
        }

        public IList<GetPostDto> GetPosts(PostQueryParameters queryParams)
        {
            IList<Post> posts = this.repo.GetPosts(queryParams).ToList();
            return posts.Select(post => postMapper.Map<GetPostDto>(post)).ToList();
        }

        public Post CreatePost(CreatePostDto postDto)
        {
            Post post = postMapper.Map<Post>(postDto);

            //post.Id = Post.Count;
            //Post.Count += 1;

            repo.CreatePost(post);
            return post;
        }

        public Post UpdatePostContent(int postId, UpdatePostContentDto postContentDto)
        {
            var mappedPost = postMapper.Map<Post>(postContentDto);
            return repo.UpdatePostContent(postId, mappedPost);
        }

        public bool DeletePostById(int postId)
        {
            return repo.DeletePostById(postId);
        }

        public GetPostDto GetPostById(int postId)
        {
            return postMapper.Map<GetPostDto>(repo.GetPostById(postId));
        }
    }
}
