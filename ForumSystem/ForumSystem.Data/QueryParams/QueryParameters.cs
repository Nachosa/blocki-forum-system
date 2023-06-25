using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.DataAccess.QueryParams
{
    public abstract class QueryParameters
    {
        //(Опционално) Може би ще е добре да направим параметрите за сортиране да са повече от един и да се сплитват, за да се сортира по няколко неща.
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
    }
}
