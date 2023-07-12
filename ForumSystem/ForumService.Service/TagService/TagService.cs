using AutoMapper;
using ForumSystem.Business.UserService;
using ForumSystem.DataAccess.Models;
using ForumSystem.DataAccess.PostRepo;
using ForumSystem.DataAccess.QueryParams;
using ForumSystem.DataAccess.TagRepo;
using ForumSystem.DataAccess.UserRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.Business.TagService
{
	public class TagService : ITagService
	{
		private readonly ITagRepository repo;
		private readonly IPostService postService;
		private readonly IUserService userService;
		public TagService(ITagRepository repo, IPostService postService, IUserService userService)
		{
			this.repo = repo;
			this.postService = postService;
			this.userService = userService;
		}

		public Tag CreateTag(Tag tag)
		{
			repo.CreateTag(tag);
			return tag;
		}

		public bool DeleteTagById(int tagId)
		{
			return repo.DeleteTagById(tagId);
		}

		public Tag GetTagById(int tagId)
		{
			return repo.GetTagById(tagId);
		}

		public IList<Tag> GetTags(TagQueryParameters queryParams)
		{
			return repo.GetTags(queryParams).ToList();
		}

		public Tag UpdateTagName(int tagId, Tag tag)
		{
			return repo.UpdateTagName(tagId, tag);
		}

		public void AddTagsToPost(string userName, int postId, string tags)
		{
			string[] tagArray = tags.Split(',');

			foreach (string tag in tagArray)
			{
				string normalizedTag = tag.Trim().ToLower();

				// Check if the tag already exists in the database
				var existingTag = repo.GetTagByName(normalizedTag);

				if (existingTag == null)
				{
					// Create a new tag if it doesn't exist
					var user = userService.GetUserByUserName(userName);
					var newTag = new Tag() { Name = normalizedTag, User = user, UserId = user.Id };
					repo.CreateTag(newTag);

					// Associate the new tag with the post
					repo.AddTagToPost(postId, newTag.Id);
				}
				else
				{
					var post = postService.GetPostById(postId);
					bool tagExist = post.Tags.Any(pt => pt.Tag.Id == existingTag.Id);
					if (tagExist is false)
					{
						// Associate the existing tag with the post
						repo.AddTagToPost(postId, existingTag.Id);

					}
				}
			}
		}

		public Tag GetTagByName(string tagName)
		{
			return repo.GetTagByName(tagName);
		}
	}
}