using ForumSystem.DataAccess.Dtos;
using ForumSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.DataAccess.ReposContracts
{
    public interface IPostRepository
    {
        public IEnumerable<Post> GetPosts();

        public Post GetPostById(int postId);

        public Post CreatePost(Post post);

        public Post UpdatePostContent(int postId, UpdatePostContentDto postContentDto);

        public Post DeletePostById(int postId);
    }
}
