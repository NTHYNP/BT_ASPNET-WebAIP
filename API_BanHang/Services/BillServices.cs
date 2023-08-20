using API_BanHang.Constant;
using API_BanHang.Entity;
using API_BanHang.Helpers;
using API_BanHang.IServices;
using Microsoft.EntityFrameworkCore;

namespace API_BanHang.Services
{
    public class BillServices : IBillServices
    {
        private readonly AppDbContext dbContext;
        public BillServices()
        {
             dbContext = new AppDbContext();
        }

        public string CreateTradeCode()
        {
            var res = DateTime.Now.ToString("yyy/mm/dd") + "_";
            var countTrade = dbContext.Bills.Count(x => x.CreateDate.Date == DateTime.Now.Date);
            if (countTrade > 0)
            {
                if (countTrade + 1 < 10)
                {
                    return res + "00" + (countTrade + 1).ToString();
                }
                else if (countTrade + 1 < 100)
                {
                    return res + "0" + (countTrade + 1).ToString();
                }
                else
                {
                    return res + (countTrade + 1).ToString();
                }
            }
            else
            {
                return res + "001";
            }
        }

        public ResponseMessage CreateBill(Bill bill)
        {
            using(var trans = dbContext.Database.BeginTransaction())
            {
                bill.CreateDate = DateTime.Now;
                bill.TradeCode = CreateTradeCode();
                var lstBillDeatail = bill.billDetails;
                bill.billDetails = null;
                dbContext.Add(bill);
                dbContext.SaveChanges();
                foreach(var billDetail in lstBillDeatail)
                {
                    if (dbContext.Products.Any(x => x.ProductId == billDetail.ProductId))
                    {
                        billDetail.BillId = bill.BillId;
                        var prod = dbContext.Products.FirstOrDefault(x => x.ProductId == billDetail.ProductId);
                        billDetail.TotalPrice = billDetail.quantity * prod.ProductPrice;
                        dbContext.Add(billDetail);
                        dbContext.SaveChanges(true);

                    }
                    else return ResponseMessage.notfound;
                }

                bill.ToltalPrice = lstBillDeatail.Sum(x => x.TotalPrice);
                dbContext.SaveChanges();
                trans.Commit();
                return ResponseMessage.success;
            }

        }

        public ResponseMessage UpdateBill (Bill bill)
        {
            using (var trans = dbContext.Database.BeginTransaction())
            {
                if (dbContext.Bills.Any(x => x.BillId == bill.BillId))
                {
                    var oldBill = dbContext.Bills.FirstOrDefault(x => x.BillId == bill.BillId);


                    var lstBillDetailOld = dbContext.BillDetails.Where(x => x.BillId == oldBill.BillId);

                    dbContext.RemoveRange(lstBillDetailOld);
                    dbContext.SaveChanges();

                    var lstBillDetail = bill.billDetails;
                    foreach (var billDetail in lstBillDetail)
                    {
                        if (dbContext.Products.Any(x => x.ProductId == billDetail.ProductId))
                        {
                            billDetail.BillId = bill.BillId;
                            var prod = dbContext.Products.FirstOrDefault(x => x.ProductId == billDetail.ProductId);
                            billDetail.TotalPrice = billDetail.quantity * prod.ProductPrice;
                            dbContext.Add(billDetail);
                            dbContext.SaveChanges();
                        }
                        else return ResponseMessage.notfound;
                    }

                    oldBill.ToltalPrice = lstBillDetail.Sum(x => x.TotalPrice);
                    oldBill.UpdateDate = DateTime.Now;
                    oldBill.BillName = bill.BillName;
                    oldBill.CustomerId = bill.CustomerId;
                    oldBill.Note = bill.Note;
                    dbContext.SaveChanges();
                    trans.Commit();
                    return ResponseMessage.success;
                }
                else return ResponseMessage.notbill;

            }
               
        }

        public ResponseMessage RemoveBill(int billId)
        {
            using(var trans = dbContext.Database.BeginTransaction())
            {
                if(dbContext.Bills.Any(x=>x.BillId == billId))
                {
                    var lstBillDetail = dbContext.BillDetails.Where(x => x.BillId == billId);
                    dbContext.RemoveRange(lstBillDetail);
                    dbContext.SaveChanges();

                    var bill = dbContext.Bills.FirstOrDefault(x => x.BillId == billId);
                    dbContext.Remove(bill);
                    dbContext.SaveChanges();

                    trans.Commit();
                    return ResponseMessage.success;
                }else return ResponseMessage.notbill;
            }
        }

        public IEnumerable<Bill> GetBill(int? year = null, int? month = null, int? fromDay = null, int? toDay = null, double? minPrice = null, double? maxPrice = null, string? keyWord = null)
        {
            var query = dbContext.Bills.Include(x => x.billDetails).OrderByDescending(X => X.CreateDate).AsQueryable();

            if (keyWord != null)
            {
                query = query.Where(x => x.BillName.ToLower().Equals(keyWord.ToLower()) || x.TradeCode.ToLower().Equals(keyWord.ToLower()) );
            }
            if (year != null)
            {
                query = query.Where(x => x.CreateDate.Year == year);
            }
            if(month != null)
            {
                query = query.Where(x => x.CreateDate.Month == month);
            }
            if(fromDay != null)
            {
                query = query.Where(x => x.CreateDate.Day >= fromDay);
            }
            if (toDay != null)
            {
                query = query.Where(x => x.CreateDate.Day <= toDay);
            }
            if(minPrice != null)
            {
                query = query.Where(x => x.ToltalPrice >= minPrice);

            }
            if (maxPrice != null)
            {
                query = query.Where(x => x.ToltalPrice <= maxPrice);

            }

            return query;
        }



        public PageResult<Bill> GetBillPage(Pageination pageination)
        {
            var query = dbContext.Bills.Include(x => x.billDetails).OrderByDescending(X => X.CreateDate).AsQueryable();

            var result = PageResult<Bill>.toPageResult(pageination,query);
            pageination.totalCount = query.Count();
            return new PageResult<Bill>(pageination,result);
        }
    }
}
