using System.Xml;

namespace API_BanHang.Entity
{
    public class BillDetail
    {
        public int BillDetailId { get; set; }
        public int quantity { get; set; }
        public string DVT { get; set; }
        public double TotalPrice { get; set; }
        public int ProductId { get; set; }
        public virtual Product? product { get; set; }

        public int BillId { get; set; }
        public virtual Bill? bill { get; set; }



    }
}
