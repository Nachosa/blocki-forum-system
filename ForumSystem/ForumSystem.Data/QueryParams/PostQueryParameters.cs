using ForumSystem.DataAccess.QueryParams;

namespace ForumSystem.Api.QueryParams
{
    public class PostQueryParameters : QueryParameters
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
