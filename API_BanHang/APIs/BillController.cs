using API_BanHang.Entity;
using API_BanHang.Helpers;
using API_BanHang.IServices;
using API_BanHang.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_BanHang.APIs
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly IBillServices billServices;
        public BillController()
        {
            billServices = new BillServices();
        }

        [HttpPost("createbill")]
        public IActionResult CreateBill(Bill bill)
        {
            var res = billServices.CreateBill(bill);
            if(res == Constant.ResponseMessage.notfound)
            {
                return Ok("Không tìm thấy sản phẩm");
            }else if(res == Constant.ResponseMessage.fail)
            {
                return Ok("Thêm Thất Bại");
            }
            return Ok("Thành Công");
        }


        [HttpPut("updatebill")]
        public IActionResult UpdateBill(Bill bill) 
        {
            var res = billServices.UpdateBill(bill);
            if (res == Constant.ResponseMessage.notbill)
            {
                return Ok("Hóa đơn không tồn tại");
            }
            else if (res == Constant.ResponseMessage.notfound)
            {
                return Ok("Không tìm thấy sản phẩm");
            }
            return Ok("Thành Công");
        }


        [HttpDelete("deletebill")]
        public IActionResult RemoveBill(int billId)
        {
            var res = billServices.RemoveBill(billId) ;
            if (res == Constant.ResponseMessage.notbill)
            {
                return Ok("Hóa đơn không tồn tại");
            }
            return Ok("Thành Công");
        }

        [HttpGet("getbill")]
        public IActionResult GetAll(int? year = null, int? month = null, int? fromDay = null, int? toDay = null, double? minPrice = null, double? maxPrice = null, string? keyWord = null,[FromQuery] Pageination pageination = null, [FromQuery] Pageination pageination1 = null)
        {
            var query = billServices.GetBill(year, month, fromDay, toDay, minPrice, maxPrice, keyWord);
            var result = PageResult<Bill>.toPageResult(pageination, query);
            pageination.totalCount = query.Count();
            var res = new PageResult<Bill>(pageination,result);
            return Ok(res);
        } 

        [HttpGet("getbillpage")]
        public IActionResult GetBillPage([FromQuery]Pageination pageination)
        {
            var res = billServices.GetBillPage(pageination);
            return Ok(res);
        }

    }


}
