using ForumSystem.Api.QueryParams;
using ForumSystem.DataAccess.Models;
using ForumSystem.DataAccess.QueryParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.Business.TagService
{
    public interface ITagService
    {
        public IList<Tag> GetTags(TagQueryParameters queryParams);

        public Tag CreateTag(Tag tag);

        public Tag UpdateTagName(int tagId, Tag tag, string userName);

        bool DeleteTagById(int tagId);

        Tag GetTagById(int tagId);
    }
}
