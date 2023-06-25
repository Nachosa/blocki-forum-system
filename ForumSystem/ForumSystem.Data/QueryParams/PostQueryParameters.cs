using ForumSystem.DataAccess.QueryParams;

namespace ForumSystem.Api.QueryParams
{
    public class PostQueryParameters : QueryParameters
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? MinDate { get; set; }
        public DateTime? MaxDate { get; set; }
        //(Опционално) Може би ще е добре да се филтрира по множество тагове.
        public string Tag { get; set; }
    }
}
