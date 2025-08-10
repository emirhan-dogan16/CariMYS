using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistence.EntityFrameworkCore
{
    public class PageResult<T>
    {
        public IEnumerable<T>? Items { get; set; }
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }

        public bool HasNextPage => CurrentPage < TotalPages;
        public bool HasPreviousPage => CurrentPage > 1;

        public bool IsFirstPage => CurrentPage == 1;
        public bool IsLastPage => CurrentPage >= TotalPages;
    }

}
