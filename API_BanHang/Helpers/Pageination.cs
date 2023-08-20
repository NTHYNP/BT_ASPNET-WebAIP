namespace API_BanHang.Helpers
{
    public class Pageination
    {
        public int pageSize { get; set; }
        public int pageNumber { get; set; }
        public int totalCount { get; set; }
        public int totalPages
        {
            get
            {
                if (this.pageSize == 0) return 0;
                var total = this.totalCount / this.pageSize;
                if (this.totalCount % this.pageSize == 0)
                {
                    return total;
                }
                else return total + 1;
            }
        }

        public Pageination()
        {
            pageNumber = 1;
            pageSize = -1;
        }
    }
}
