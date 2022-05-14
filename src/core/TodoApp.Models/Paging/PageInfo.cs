namespace TodoApp.Models.Paging
{
    public class PageInfo
    {
        public int PageNo { get; }
        public int PageSize { get; }
        public int TotalItems { get; }
        public int TotalPages { get; }

        public bool HasPrevious => PageNo > 1;
        public bool HasNext => PageNo < TotalPages;

        public PageInfo(int pageNo, int pageSize, int totalItems)
        {
            PageNo = pageNo;
            PageSize = pageSize;
            TotalItems = totalItems;
            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
        }
    }
}
