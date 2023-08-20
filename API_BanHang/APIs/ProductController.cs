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
    public class ProductController : ControllerBase
    {
        private readonly IProductServices productServices;
        public ProductController()
        {
            productServices = new ProductServices();
        }

        [HttpPost("addproduct")]
        public  IActionResult AddProduct([FromBody] Product product)
        {
            var res = productServices.AddProduct(product);
            if (res == Constant.ResponseMessage.notfound) { return Ok("Loại Sản Phẩm Không Tồn Tại"); }
            return Ok("Thành Công");
        }

        [HttpPut("updateproduct")]
        public IActionResult UpdateProduct([FromBody] Product product)
        {
            var res = productServices.UpdateProduct(product);
            if (res == Constant.ResponseMessage.notfound) { return Ok("Loại Sản Phẩm Không Tồn Tại"); }
            if (res == Constant.ResponseMessage.fail) return Ok("Không tìm thấy sản phẩm");
            return Ok("Thành Công");
        }

        [HttpDelete("deleteproduct")]
        public IActionResult DeleteProduct([FromQuery]int productId) 
        {
            var res = productServices.DeleteProduct(productId);
            if (res == Constant.ResponseMessage.notfound) return Ok("Không tìm thấy sản phẩm");
            return Ok("Thành công");
        }

        [HttpGet("getproduct")]
        public IActionResult GetProduct([FromQuery] string? productName = null, [FromQuery] Pageination pageination = null)
        {
            var query = productServices.GetProducts(productName);
            var result = PageResult<Product>.toPageResult(pageination, query);
            pageination.totalCount = query.Count();
            var res = new PageResult<Product>(pageination, result);
            return Ok(res);
        }
    }
}
