﻿using ForumSystem.Api.QueryParams;
using ForumSystem.DataAccess.Models;
using ForumSystem.DataAccess.QueryParams;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.DataAccess.TagRepo
{
    public class TagRepository : ITagRepository
    {
        private readonly ForumSystemContext forumDb;

        public TagRepository(ForumSystemContext forumDb)
        {
            this.forumDb = forumDb;
        }

        public Tag CreateTag(Tag tag)
        {
            forumDb.Tags.Add(tag);
            forumDb.SaveChanges();
            return tag;
        }

        public bool DeleteTagById(int tagId)
        {
            var tag = forumDb.Tags.FirstOrDefault(tag => tag.Id == tagId);

            if (tag == null || tag.IsDeleted)
                throw new ArgumentNullException($"Tag with id={tagId} doesn't exist.");
            else
                tag.IsDeleted = true;

            forumDb.SaveChanges();

            return true;
        }

        public Tag GetTagById(int tagId)
        {
            var tag = forumDb.Tags.Include(t => t.Name).FirstOrDefault(tag => tag.Id == tagId);

            if (tag == null || tag.IsDeleted)
                throw new ArgumentNullException($"Tag with id={tagId} doesn't exist.");
            else
                return tag;
        }

        public IEnumerable<Tag> GetTags(TagQueryParameters queryParameters)
        {
            List<Tag> tagsToProcess = new List<Tag>(forumDb.Tags.Where(t => t.IsDeleted == false).Include(t => t.Name));
            tagsToProcess = FilterBy(queryParameters, tagsToProcess);
            tagsToProcess = SortBy(queryParameters, tagsToProcess);
            return tagsToProcess;
        }

        public Tag UpdateTagName(int tagId, Tag tag, string userName)
        {
            var t = forumDb.Tags.FirstOrDefault(t => t.Id == tagId);

            if (t == null || t.IsDeleted)
                throw new ArgumentNullException($"Tag with id={tagId} doesn't exist.");
            // should we allow regular users to create new tags or do we restrict it to admins only?
            else
                t.Name = tag.Name;

            forumDb.SaveChanges();

            return t;
        }

        public List<Tag> FilterBy(TagQueryParameters filterParameters, List<Tag> tags)
        {
            if (!string.IsNullOrEmpty(filterParameters.Name))
            {
                tags = tags.FindAll(tag => tag.Name.Contains(filterParameters.Name, StringComparison.InvariantCultureIgnoreCase));
            }

            return tags;
        }

        public List<Tag> SortBy(TagQueryParameters sortParameters, List<Tag> tags)
        {
            if (!string.IsNullOrEmpty(sortParameters.SortBy))
            {
                if (sortParameters.SortBy.Equals("name", StringComparison.InvariantCultureIgnoreCase))
                {
                    tags = tags.OrderBy(tag => tag.Name).ToList();
                }

                if (sortParameters.SortBy.Equals("postCount", StringComparison.InvariantCultureIgnoreCase))
                {
                    tags = tags.OrderBy(tag => tag.Posts.Count).ToList();
                }

                if (!string.IsNullOrEmpty(sortParameters.SortOrder) && sortParameters.SortOrder.Equals("desc", StringComparison.InvariantCultureIgnoreCase))
                {
                    tags.Reverse();
                }
            }

            return tags;
        }
    }
}