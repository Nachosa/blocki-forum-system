using ForumSystem.Api.QueryParams;
using ForumSystem.DataAccess.Exceptions;
using ForumSystem.DataAccess.Models;
using ForumSystem.DataAccess.TagRepo;
using Microsoft.EntityFrameworkCore;
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
        private readonly ITagRepository tagRepo;

        public PostRepository(ForumSystemContext forumDb, ITagRepository tagRepo)
        {
            this.forumDb = forumDb;
            this.tagRepo = tagRepo;
        }

        public IEnumerable<Post> GetPosts(PostQueryParameters queryParameters)
        {
            List<Post> postsToProcess = new List<Post>(forumDb.Posts.Where(p => p.IsDeleted == false)
                                                                    .Include(p => p.Likes.Where(l => l.IsDeleted == false))
                                                                    .Include(p => p.User) 
                                                                    .Include(p => p.Comments.Where(c => c.IsDeleted == false))
                                                                    .Include(p => p.Tags).ThenInclude(pt => pt.Tag).Where(t => t.IsDeleted == false));
            postsToProcess = FilterBy(queryParameters, postsToProcess);
            postsToProcess = SortBy(queryParameters, postsToProcess);
            return postsToProcess;
        }

        public ICollection<Post> GetUserPosts(int userId, PostQueryParameters queryParameters)
        {
            List<Post> userPosts = forumDb.Posts.Where(p => p.UserId == userId && p.IsDeleted == false)
                                                .Include(p => p.Likes.Where(l => l.IsDeleted == false))
                                                .ToList();
            userPosts = FilterBy(queryParameters, userPosts);
            userPosts = SortBy(queryParameters, userPosts);
            return userPosts;

        }

        public int GetPostsCount()
        {
            int activePostsCount = forumDb.Posts.Count(p => p.IsDeleted == false);
            return activePostsCount;
        }
        public Post CreatePost(Post post)
        {
            forumDb.Posts.Add(post);
            forumDb.SaveChanges();
            return post;
        }

        public bool LikePost(Post post, User user)
        {
            forumDb.Likes.Add(new Like { PostId = post.Id, UserId = user.Id });
            forumDb.SaveChanges();
            return true;
        }

        public Like GetLike(int postId, int userId)
        {
            var like = forumDb.Likes.FirstOrDefault(l => l.PostId == postId && l.UserId == userId && l.IsDeleted == false);
            return like;
        }

        public bool UnikePost(Like like)
        {
            like.IsDeleted = true;
            forumDb.SaveChanges();
            return true;
        }

        public bool TagPost(Post post, Tag tag)
        {
            forumDb.PostTags.Add(new PostTag { PostId = post.Id, TagId = tag.Id });
            forumDb.SaveChanges();
            return true;
        }

        //Може би ще е добре тук да се преизползва GetPostById, но пък ще е ненужно инклудването на Id и User?
        //Изтриване и на лайковете и коментарите на поста?
        public bool DeletePostById(int postId)
        {
            var post = forumDb.Posts.FirstOrDefault(post => post.Id == postId);
            if (post == null || post.IsDeleted)
                throw new EntityNotFoundException($"Post with id={postId} doesn't exist.");
            else
                post.DeletedOn = DateTime.Now;
            post.IsDeleted = true;
            forumDb.SaveChanges();
            return true;
        }

        public Post GetPostById(int postId)
        {
            //Include преди FirstOrDefault ми се струва много бавно.
            var post = forumDb.Posts.Include(p => p.Likes.Where(l => l.IsDeleted == false))
                                    .Include(p => p.User)
                                    .Include(p => p.Comments).ThenInclude(c => c.User).Where(c => c.IsDeleted == false)
                                    .Include(p => p.Tags).ThenInclude(pt => pt.Tag).Where(t => t.IsDeleted == false)
                                    .FirstOrDefault(post => post.Id == postId);
			//var post = forumDb.Posts.Include(p => p.Likes.Where(l => l.IsDeleted == false))
			//						.Include(p => p.User)
			//						.Include(p => p.Comments.Where(c => c.IsDeleted == false))
			//						.Include(p => p.Tags).ThenInclude(pt => pt.Tag).Where(t => t.IsDeleted == false)
			//						.FirstOrDefault(post => post.Id == postId);
			if (post == null || post.IsDeleted)
                throw new EntityNotFoundException($"Post with id={postId} doesn't exist.");
            else
                return post;
        }

        public Post UpdatePostContent(Post newPost, Post currPost)
        {
            currPost.Content = newPost.Content;
            forumDb.SaveChanges();
            return currPost;
        }

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

            if (filterParameters.MinDate is not null)
            {
                posts = posts.FindAll(post => post.CreatedOn >= filterParameters.MinDate);
            }

            if (filterParameters.MaxDate is not null)
            {
                posts = posts.FindAll(post => post.CreatedOn <= filterParameters.MaxDate);
            }

            if (filterParameters.Tag is not null)
            {
                var tag = tagRepo.GetTagByName(filterParameters.Tag);
                if (tag != null)
                    posts = posts.FindAll(post => post.Tags.Any(pt => pt.Id == tag.Id));
                //Тук не съм сигурен, че това е най-рентабилния вариант ако няма такъв таг.
                else
                    posts.Clear();
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
                if (sortParameters.SortBy.Equals("likes", StringComparison.InvariantCultureIgnoreCase))
                {
                    posts = posts.OrderBy(post => post.Likes.Count).ToList();
                }
                if (sortParameters.SortBy.Equals("comments", StringComparison.InvariantCultureIgnoreCase))
                {
                    posts = posts.OrderBy(post => post.Comments.Count).ToList();
                }

                if (!string.IsNullOrEmpty(sortParameters.SortOrder) && sortParameters.SortOrder.Equals("desc", StringComparison.InvariantCultureIgnoreCase))
                {
                    posts.Reverse();
                }
            }

            return posts;
        }

        //public ICollection<Post> GetPostsWithTag(string tag1)
        //{
        //    var posts = forumDb.Posts.Include(p => p.Tags).Where(p => p.Tags.Any(t => t.Tag.Name == tag1));
        //    return posts.ToList();
        //}

        //public Tag GetTagWithName(string name)
        //{
        //    return forumDb.Tags.FirstOrDefault(t => t.Name == name);
        //}
    }
}
