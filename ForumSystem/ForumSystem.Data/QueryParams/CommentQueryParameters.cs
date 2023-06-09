using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.DataAccess.QueryParams
{
    public class CommentQueryParameters : QueryParameters
    {
        public DateTime? MinDate { get; set; }
        public DateTime? MaxDate { get; set; }

        public string Content { get; set; }
    }
}
