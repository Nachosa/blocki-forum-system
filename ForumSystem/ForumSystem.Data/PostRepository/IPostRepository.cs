using ForumSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.DataAccess.PostRepository
{
    public interface IPostRepository
    {
        public IEnumerable<Post> GetPosts();

        public Post GetPostById(int postId);

        public Post CreatePost(Post post);

        public Post UpdatePostContent(int postId, Post postContentDto);

        public Post DeletePostById(int postId);
    }
}
