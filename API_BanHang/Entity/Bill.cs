namespace API_BanHang.Entity
{
    public class Bill
    {
        public int BillId { get; set; }
        public string BillName { get; set;}
        public string? TradeCode { get; set;}
        public DateTime CreateDate { get; set;}
        public DateTime? UpdateDate { get; set;}
        public string Note { get; set;}
        public double? ToltalPrice { get; set;}

        public int? CustomerId { get; set;}
        public virtual Customer? customer { get; set;}
        
        public IEnumerable<BillDetail> billDetails { get; set;}
    }   
}
