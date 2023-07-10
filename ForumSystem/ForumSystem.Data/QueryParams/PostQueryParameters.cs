using ForumSystem.DataAccess.QueryParams;

namespace ForumSystem.Api.QueryParams
{
    public class PostQueryParameters : QueryParameters
    {
		public DateTime? MaxDate { get; set; }

		public DateTime? MinDate { get; set; }

		public string Content { get; set; }

		public string CreatedBy { get; set; }

		public string Tag { get; set; }

		public string Title { get; set; }
	}
}