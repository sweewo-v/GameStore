using System;
using System.Collections.Generic;

namespace GameStore.Application.ViewModels.Pagination
{
    public class PageViewModel<T>
    {
        public int PageNumber { get; }
        public int TotalPages { get; }
        public IEnumerable<T> Items { get; }

        public PageViewModel(IEnumerable<T> items, int pageNumber, int pageSize, int count)
        {
            PageNumber = pageNumber;
            Items = items;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        public bool HasPreviousPage => (PageNumber > 1);

        public bool HasNextPage => (PageNumber < TotalPages);
    }
}
