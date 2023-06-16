﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumSystem.Api.QueryParams;
using ForumSystem.DataAccess.Models;
using ForumSystem.DataAccess.PostRepo;
using ForumSystem.DataAccess.QueryParams;
using Microsoft.EntityFrameworkCore;

namespace ForumSystem.DataAccess.CommentRepo
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ForumSystemContext forumDb;

        public CommentRepository(ForumSystemContext forumDb)
        {
            this.forumDb = forumDb;
        }

        public IEnumerable<Comment> GetAllComments()
        {
            return forumDb.Comments;
        }

        public Comment CreateComment(Comment comment)
        {
            comment.Id = forumDb.Comments.OrderByDescending(c => c.Id).FirstOrDefault().Id + 1;
            forumDb.Comments.Add(comment);
            forumDb.SaveChanges();
            return comment;
        }

        public Comment UpdateComment(int commentId, Comment comment)
        {
            var existingComment = FindCommentById(commentId);

            if (existingComment == null)
            {
                return comment;
            }

            existingComment.Content = comment.Content;

            return comment;
        }

        //След базата не работи.
        //public void DeleteComment(Comment comment)
        //{
        //    comments.Remove(comment);
        //}

        public Comment FindCommentById(int commentId)
        {
            var comment = forumDb.Comments.Include(c=>c.Likes).FirstOrDefault(comment => comment.Id == commentId);
            return comment ?? throw new ArgumentNullException($"Comment with id={commentId} doesn't exist.");
        }

        public IEnumerable<Comment> FindCommentsByPostId(int postId)
        {
            var post = forumDb.Posts.FirstOrDefault(post => post.Id == postId);
            return post.Comments ?? throw new ArgumentNullException($"Post with id={postId} doesn't exist.");
        }

        public bool DeleteCommentById(int commentId)
        {
            var comment = forumDb.Comments.FirstOrDefault(comment => comment.Id == commentId);
            if (comment == null)
                throw new ArgumentNullException($"Comment with id={commentId} doesn't exist.");
            else
                comment.IsDeleted = true;
                forumDb.SaveChanges();
            return true;
        }

        public List<Comment> FilterBy(CommentQueryParameters filterParameters, List<Comment> comments)
        {
            if (filterParameters.MinDate <= filterParameters.MaxDate)
            {
                comments = comments.FindAll(comment => comment.CreatedOn >= filterParameters.MinDate
                                           && comment.CreatedOn <= filterParameters.MaxDate);
            }

            if (!string.IsNullOrEmpty(filterParameters.Content))
            {
                comments = comments.FindAll(post => post.Content.Contains(filterParameters.Content, StringComparison.InvariantCultureIgnoreCase));
            }

            return comments;
        }

        public List<Comment> SortBy(CommentQueryParameters sortParameters, List<Comment> comments)
        {
            if (!string.IsNullOrEmpty(sortParameters.SortBy))
            {
                if (sortParameters.SortBy.Equals("date", StringComparison.InvariantCultureIgnoreCase))
                {
                    comments = comments.OrderBy(comment => comment.CreatedOn).ToList();
                }

                if (!string.IsNullOrEmpty(sortParameters.SortOrder) && sortParameters.SortOrder.Equals("desc", StringComparison.InvariantCultureIgnoreCase))
                {
                    comments.Reverse();
                }
            }

            return comments;
        }
    }
}
