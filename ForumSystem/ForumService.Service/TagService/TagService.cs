using AutoMapper;
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

        public TagService(ITagRepository repo)
        {
            this.repo = repo;
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

        public Tag UpdateTagName(int tagId, Tag tag, string userName)
        {
            return repo.UpdateTagName(tagId, tag, userName);
        }

		public void AddTagsToPost(int postId, string tags)
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
					var newTag = new Tag() { Name = normalizedTag };

					repo.CreateTag(newTag);

					// Associate the new tag with the post
					repo.AddTagToPost(postId, newTag.Id);
				}
				else
				{
					// Associate the existing tag with the post
					repo.AddTagToPost(postId, existingTag.Id);
				}
			}
		}
	}
}