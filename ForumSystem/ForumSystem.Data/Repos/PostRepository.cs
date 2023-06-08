﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumSystem.DataAccess.Dtos;
using ForumSystem.DataAccess.Models;
using ForumSystem.DataAccess.ReposContracts;

namespace ForumSystem.DataAccess.Repos
{
    public class PostRepository : IPostRepository
    {
        public static IList<Post> posts = new List<Post>();

        public IEnumerable<Post> GetPosts()
        {
            return new List<Post>(posts);
        }

        public Post CreatePost(Post post)
        {
            posts.Add(post);
            return post;
        }

        public Post DeletePostById(int postId)
        {
            var post = posts.FirstOrDefault(post => post.Id == postId);
            if (post == null)
                throw new ArgumentNullException($"Post with id={postId} doesn't exist.");
            else
                posts.Remove(post);
            return post;
        }

        public Post GetPostById(int postId)
        {
            var post = posts.FirstOrDefault(post => post.Id == postId);
            return post ?? throw new ArgumentNullException($"Post with id={postId} doesn't exist.");
        }

        public Post UpdatePostContent(int postId, UpdatePostContentDto postContentDto)
        {
            var post = posts.FirstOrDefault(post => post.Id == postId);
            if (post == null)
                throw new ArgumentNullException($"Post with id={postId} doesn't exist.");
            else
                post.Content = postContentDto.Content;
            return post;
        }
    }
}
