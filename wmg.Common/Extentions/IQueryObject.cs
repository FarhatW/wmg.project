using System;
using System.Collections.Generic;
using System.Text;

namespace wmg.Common.Extentions
{
    public interface IQueryObject
    {
        string SortBy { get; set; }
        bool IsSortAscending { get; set; }

        int Page { get; set; }
        int PageSize { get; set; }
    }
}
