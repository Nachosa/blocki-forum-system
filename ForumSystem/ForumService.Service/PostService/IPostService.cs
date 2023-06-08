using ForumSystem.Api.QueryParams;
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
        IList<GetPostDto> GetPosts(PostQueryParameters queryParams);

        Post CreatePost(CreatePostDto postDto);

        Post UpdatePostContent(int postId, UpdatePostContentDto postContentDto);

        bool DeletePostById(int postId);

        GetPostDto GetPostById(int postId);
    }
}
