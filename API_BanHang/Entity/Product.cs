namespace API_BanHang.Entity
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public string ProductDescription { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string Symbol { get; set;}

        public int ProductTypeId { get; set; }
        public virtual ProductType? ProductType { get; set; }

    }
}
