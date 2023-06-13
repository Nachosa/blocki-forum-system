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

        public Post CreatePost(Post post);

        Post UpdatePostContent(int postId, UpdatePostContentDto postContentDto);

        bool DeletePostById(int postId);

        Post GetPostById(int postId);
    }
}
