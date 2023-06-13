﻿using ForumSystem.Api.QueryParams;
using ForumSystem.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.DataAccess.PostRepo
{
    public class PostRepository : IPostRepository
    {
        private readonly ForumSystemContext forumDb;

        public PostRepository(ForumSystemContext forumDb)
        {
            this.forumDb = forumDb;
        }

        public IEnumerable<Post> GetPosts(PostQueryParameters queryParameters)
        {
            List<Post> postsToProcess = new List<Post>(forumDb.Posts.Where(p => p.IsDeleted == false));
            postsToProcess = FilterBy(queryParameters, postsToProcess);
            postsToProcess = SortBy(queryParameters, postsToProcess);
            return postsToProcess;
        }

        public Post CreatePost(Post post)
        {
            forumDb.Posts.Add(post);
            forumDb.SaveChanges();
            return post;
        }

        public bool DeletePostById(int postId)
        {
            var post = forumDb.Posts.FirstOrDefault(post => post.Id == postId);
            if (post == null || post.IsDeleted)
                throw new ArgumentNullException($"Post with id={postId} doesn't exist.");
            else
                post.IsDeleted = true;
            forumDb.SaveChanges();
            return true;
        }

        public Post GetPostById(int postId)
        {
            var post = forumDb.Posts.FirstOrDefault(post => post.Id == postId);
            if (post == null || post.IsDeleted)
                throw new ArgumentNullException($"Post with id={postId} doesn't exist.");
            else
                return post;
        }

        public Post UpdatePostContent(int postId, Post newPost, string userName)
        {
            var post = forumDb.Posts.FirstOrDefault(post => post.Id == postId);
            if (post == null || post.IsDeleted)
                throw new ArgumentNullException($"Post with id={postId} doesn't exist.");
            else
                post.Content = newPost.Content;
            forumDb.SaveChanges();
            return post;
        }

        //(Опционално) Филтриране по дата на създаване.
        public List<Post> FilterBy(PostQueryParameters filterParameters, List<Post> posts)
        {
            if (!string.IsNullOrEmpty(filterParameters.Title))
            {
                posts = posts.FindAll(post => post.Title.Contains(filterParameters.Title, StringComparison.InvariantCultureIgnoreCase));
            }

            if (!string.IsNullOrEmpty(filterParameters.Content))
            {
                posts = posts.FindAll(post => post.Content.Contains(filterParameters.Content, StringComparison.InvariantCultureIgnoreCase));
            }

            if (!(filterParameters.MinDate == null))
            {
                posts = posts.FindAll(post => post.CreatedOn >= filterParameters.MinDate);
            }

            if (!(filterParameters.MaxDate == null))
            {
                posts = posts.FindAll(post => post.CreatedOn <= filterParameters.MaxDate);
            }

            return posts;
        }

        //(Опционално) Може би ще е добре да направим параметрите за сортиране да са повече от един и да се сплитват, за да се сортира по няколко неща.
        public List<Post> SortBy(PostQueryParameters sortParameters, List<Post> posts)
        {
            if (!string.IsNullOrEmpty(sortParameters.SortBy))
            {
                if (sortParameters.SortBy.Equals("title", StringComparison.InvariantCultureIgnoreCase))
                {
                    posts = posts.OrderBy(post => post.Title).ToList();
                }
                if (sortParameters.SortBy.Equals("date", StringComparison.InvariantCultureIgnoreCase))
                {
                    posts = posts.OrderBy(post => post.CreatedOn).ToList();
                }

                if (!string.IsNullOrEmpty(sortParameters.SortOrder) && sortParameters.SortOrder.Equals("desc", StringComparison.InvariantCultureIgnoreCase))
                {
                    posts.Reverse();
                }
            }

            return posts;
        }
    }
}
