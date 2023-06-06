using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumSystem.DataAccess.Dtos;
using ForumSystem.DataAccess.Models;

namespace ForumSystem.DataAccess.Helpers
{
    public class PostMapper
    {
        public Post MapCreate(CreatePostDto postDto)
        {
            return new Post
            {
                UserId = postDto.UserId,
                Title = postDto.Title,
                Content = postDto.Content,
            };
        }

        public GetPostDto MapGet(Post post)
        {
            return new GetPostDto
            {
                Title = post.Title,
                Content = post.Content,
                Likes = post.Likes,
                Dislikes = post.Dislikes
            };
        }
    }
}
