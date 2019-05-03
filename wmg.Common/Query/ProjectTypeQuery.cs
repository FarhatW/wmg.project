using System;
using System.Collections.Generic;
using System.Text;
using wmg.Common.Extentions;

namespace wmg.Common.Query
{
    public class ProjectTypeQuery : IQueryObject
    {
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string LbProject { get; set; }
        public int Id { get; set; }

    }
}
