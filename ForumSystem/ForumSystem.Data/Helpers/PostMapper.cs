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
        public Post Map(CreatePostDto postDto)
        {
            return new Post
            {
                Title = postDto.Title,
                Content = postDto.Content,
            };
        }
    }
}
