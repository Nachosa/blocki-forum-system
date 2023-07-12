using ForumSystem.Api.QueryParams;
using ForumSystem.DataAccess.Models;
using ForumSystem.DataAccess.QueryParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.DataAccess.TagRepo
{
    public interface ITagRepository
    {
        IEnumerable<Tag> GetTags(TagQueryParameters queryParameters);

        Tag GetTagById(int tagId);

        Tag GetTagByName(string name);

        Tag CreateTag(Tag tag);

        Tag UpdateTagName(int tagId, Tag tag);

        bool DeleteTagById(int tagId);

		public void AddTagToPost(int postId, int tagId);
	}
}
