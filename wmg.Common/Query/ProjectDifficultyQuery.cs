using System;
using System.Collections.Generic;
using System.Text;
using wmg.Common.Extentions;

namespace wmg.Common.Query
{
    public class ProjectDifficultyQuery : IQueryObject
    {
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string LbDifficulty { get; set; }
        public int Id { get; set; }

    }
}
