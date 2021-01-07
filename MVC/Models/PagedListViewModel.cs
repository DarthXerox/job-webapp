using System.Collections.Generic;

namespace MVC.Models
{
    public class PagedListViewModel<T>
    {
        public PaginationViewModel Pagination { get; set; }
        public IEnumerable<T> List { get; set; }

        public PagedListViewModel(PaginationViewModel pagination, IEnumerable<T> list)
        {
            Pagination = pagination;
            List = list;
        }
    }
}
