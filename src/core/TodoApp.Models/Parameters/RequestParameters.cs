namespace TodoApp.Models.Parameters
{
    public abstract record RequestParameters
    {
        private const int MaxPageSize = 50;

        public int PageNo { get; init; } = 1;

        private int _pageSize = 10;
        public int PageSize 
        {
            get => _pageSize;
            init
            {
                _pageSize = value > MaxPageSize ? MaxPageSize : value;
            }
        }

        public string? Search { get; init; }
        public string? OrderBy { get; init; }
    }
}
