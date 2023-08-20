using API_BanHang.Constant;
using API_BanHang.Entity;
using API_BanHang.Helpers;

namespace API_BanHang.IServices
{
    public interface IBillServices
    {
        public ResponseMessage CreateBill(Bill bill);
        public ResponseMessage UpdateBill(Bill bill);
        public ResponseMessage RemoveBill(int billId);
        public IEnumerable<Bill> GetBill(int? year = null, int? month = null,
            int? fromDay = null, int? toDay = null, double? minPrice = null,
            double? maxPrice = null, string? keyWord = null);

        public PageResult<Bill> GetBillPage(Pageination pageination);
        
    }
}
