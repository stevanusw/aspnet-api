namespace TodoApp.Models.Paging
{
    public class PagedList<T> : List<T>
    {
        public PageInfo PageInfo { get; }

        public PagedList(List<T> items, int pageNo, int pageSize, int totalItems)
        {
            PageInfo = new PageInfo(pageNo, pageSize, totalItems);

            AddRange(items);
        }
    }
}
