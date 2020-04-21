namespace Shop.API.Persistence.Helpers
{
    public class ProductParams
    {
        private const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int pageSize = 10;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value >= maxPageSize) ? maxPageSize : value; }
        }
        public string CategoryName { get; set; }
        public string OrderBy { get; set; }
    }
}