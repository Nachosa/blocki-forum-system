using ForumSystem.DataAccess.Dtos;
using ForumSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.Business
{
    public interface IPostService
    {
        public IList<Post> GetAllPosts();

        public Post CreatePost(CreatePostDto postDto);

        public bool UpdatePost(int postId, Post post);

        public void DeletePost(Post post);

        public Post FindPostById(int postId);
    }
}
