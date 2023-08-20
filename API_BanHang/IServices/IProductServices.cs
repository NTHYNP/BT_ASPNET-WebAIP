using API_BanHang.Constant;
using API_BanHang.Entity;
using API_BanHang.Helpers;

namespace API_BanHang.IServices
{
    public interface IProductServices
    {
        public ResponseMessage AddProduct(Product product);
        public ResponseMessage UpdateProduct(Product product);
        public ResponseMessage DeleteProduct(int productId);
        public IEnumerable<Product> GetProducts(string? productName = null);
    }
}
