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
        public IList<GetPostDto> GetPosts();

        public Post CreatePost(CreatePostDto postDto);

        public Post UpdatePostContent(int postId, UpdatePostContentDto postContentDto);

        public bool DeletePostById(int postId);

        public GetPostDto GetPostById(int postId);
    }
}
