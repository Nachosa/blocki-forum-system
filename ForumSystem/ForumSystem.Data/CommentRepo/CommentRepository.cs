using ForumSystem.Api.QueryParams;
using ForumSystem.DataAccess.Exceptions;
using ForumSystem.DataAccess.Models;
using ForumSystem.DataAccess.PostRepo;
using ForumSystem.DataAccess.QueryParams;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.DataAccess.CommentRepo
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ForumSystemContext forumDb;

        public CommentRepository(ForumSystemContext forumDb)
        {
            this.forumDb = forumDb;
        }

        public bool DeleteCommentById(int commentId)
        {
            var comment = forumDb.Comments.FirstOrDefault(c => c.Id == commentId);

            if (comment == null)
            {
                return false;
            }

            comment.DeletedOn = DateTime.Now;
            comment.IsDeleted = true;
            forumDb.SaveChanges();
            return true;
        }

        public Comment CreateComment(Comment comment)
        {
            //comment.Id = forumDb.Comments
            //    .Where(c => c.IsDeleted == false)
            //    .OrderByDescending(c => c.Id)
            //    .Select(c => c.Id)
            //    .FirstOrDefault() + 1;

            forumDb.Comments.Add(comment);
            forumDb.SaveChanges();
            return comment;
        }

        public Comment GetCommentById(int commentId)
        {
            return forumDb.Comments
                .Include(c => c.Likes)
                .Include(c => c.User)
                .FirstOrDefault(c => c.Id == commentId);
        }

        public Comment UpdateCommentContent(Comment comment, int commentId)
        {
            var commentToUpdate = forumDb.Comments.FirstOrDefault(c => c.Id == commentId);

            commentToUpdate.Content = comment.Content ?? commentToUpdate.Content;
            forumDb.SaveChanges();
            return commentToUpdate;
        }

        public IEnumerable<Comment> GetComments(CommentQueryParameters queryParameters)
        {
            var comments = new List<Comment>(forumDb.Comments
                .Where(comment => comment.IsDeleted == false)
                .Include(comment => comment.Likes.Where(like => like.IsDeleted == false))
                .Include(comment => comment.User)
                .Where(comment => comment.User.IsDeleted == false));

            comments = FilterBy(queryParameters, comments);
            comments = SortBy(queryParameters, comments);
            return comments;
        }

        public IEnumerable<Comment> GetCommentsByPostId(int postId)
        {
            var post = forumDb.Posts.FirstOrDefault(post => post.Id == postId);

            return post.Comments;
        }

        public List<Comment> FilterBy(CommentQueryParameters queryParameters, List<Comment> comments)
        {
            if (queryParameters.MinDate <= queryParameters.MaxDate)
            {
                comments = comments.FindAll(comment => comment.CreatedOn >= queryParameters.MinDate && comment.CreatedOn <= queryParameters.MaxDate);
            }

            if (string.IsNullOrEmpty(queryParameters.Content) == false)
            {
                comments = comments.FindAll(post => post.Content.Contains(queryParameters.Content, StringComparison.InvariantCultureIgnoreCase));
            }

            return comments;
        }

        public List<Comment> SortBy(CommentQueryParameters queryParameters, List<Comment> comments)
        {
            if (string.IsNullOrEmpty(queryParameters.SortBy) == false)
            {
                if (queryParameters.SortBy.Equals("date", StringComparison.InvariantCultureIgnoreCase))
                {
                    comments = comments.OrderBy(comment => comment.CreatedOn).ToList();
                }

                if (queryParameters.SortOrder.Equals("desc", StringComparison.InvariantCultureIgnoreCase) && string.IsNullOrEmpty(queryParameters.SortOrder) == false)
                {
                    comments.Reverse();
                }
            }

            return comments;
        }
    }
}