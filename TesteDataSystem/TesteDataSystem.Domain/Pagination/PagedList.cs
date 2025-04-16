using System;
using System.Collections.Generic;

namespace TesteDataSystem.Domain.Pagination
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public PagedList(IEnumerable<T> items, int currentPage, int pageSize, int totalCount)
        {
            CurrentPage = currentPage;
            TotalPages = (int) Math.Ceiling(totalCount / (double) pageSize);
            PageSize = pageSize;
            TotalCount = totalCount;

            AddRange(items);
        }

        public PagedList(IEnumerable<T> items, int currentPage, int totalPages, int pageSize, int totalCount)
        {
            CurrentPage = currentPage;
            TotalPages = totalPages;
            PageSize = pageSize;
            TotalCount = totalCount;

            AddRange(items);
        }
    }
}
