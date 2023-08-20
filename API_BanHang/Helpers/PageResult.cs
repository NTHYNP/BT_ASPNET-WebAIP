namespace API_BanHang.Helpers
{
    public class PageResult<T>
    {
        public Pageination Pageination { get; set; }
        public IEnumerable<T> data { get; set; }

        public PageResult()
        {

        }
        public PageResult (Pageination pageination, IEnumerable<T> data)
        {
            Pageination = pageination;
            this.data = data;
        }
        public static IEnumerable<T> toPageResult(Pageination pageination, IEnumerable<T> data)
        {
            if(pageination.pageSize > 0)
            {
                pageination.pageNumber = pageination.pageNumber < 1 ? 1 : pageination.pageNumber;

                data = data.Skip(pageination.pageSize * (pageination.pageNumber - 1)).Take(pageination.pageSize).AsQueryable();
            }
            
            return data;
        }
        
    }
}
