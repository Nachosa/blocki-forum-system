using ForumSystem.Api.QueryParams;
using ForumSystemDTO.PostDTO;
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
        public IList<Post> GetPosts(PostQueryParameters queryParams);

        //public ICollection<Post> GetPostsWithTag(string tag1);

        public Post CreatePost(Post post, string userName);

        public bool LikePost(int postId, string userName);

        public bool UnlikePost(int postId, string userName);

        bool TagPost(int postId, string userName, Tag tag);

        public Post UpdatePostContent(int postId, Post post, string userName);

        bool DeletePostById(int postId, string userName);

        Post GetPostById(int postId);
    }
}
