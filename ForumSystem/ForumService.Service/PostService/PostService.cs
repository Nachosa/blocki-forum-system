using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumSystem.DataAccess.Models;
using ForumSystem.DataAccess;
using ForumSystem.DataAccess.Helpers;
using ForumSystem.DataAccess.Dtos;

namespace ForumSystem.Business
{
    public class PostService : IPostService
    {
        private readonly IForumSystemRepository repo;
        private readonly PostMapper postMapper;

        public PostService (IForumSystemRepository repo, PostMapper postMapper) 
        {
            this.repo = repo;
            this.postMapper = postMapper;
        }

        public IList<Post> GetAllPosts()
        {
            return this.repo.GetAllPosts().ToList();
        }

        public Post CreatePost(CreatePostDto postDto)
        {
            Post post = postMapper.MapCreate(postDto);

            post.Id = Post.Count;
            post.Likes = 0;
            post.Dislikes = 0;
            Post.Count += 1;

            repo.CreatePost(post);
            return post;
        }

        public Post UpdatePostContent(Post post, UpdatePostContentDto postContentDto)
        {
            return repo.UpdatePostContent(post, postContentDto);
        }

        public Post DeletePostById(int postId)
        {
            return repo.DeletePostById(postId);
        }

        public Post FindPostById(int postId)
        {
            return repo.FindPostById(postId);
        }
    }
}
