namespace GameStore.Application.ViewModels.Pagination
{
    public class PageRequestViewModel
    {
        public int Page { get; }
        public int PageSize { get; }
        public PageRequestViewModel(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }

    }
}
