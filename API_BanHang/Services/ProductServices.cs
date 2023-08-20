using API_BanHang.Constant;
using API_BanHang.Entity;
using API_BanHang.Helpers;
using API_BanHang.IServices;
using Microsoft.EntityFrameworkCore;

namespace API_BanHang.Services
{
    public class ProductServices : IProductServices
    {

        private readonly AppDbContext dbContext;
        public ProductServices()
        {
            dbContext = new AppDbContext();
        }
        public ResponseMessage AddProduct(Product product)
        {
             if(dbContext.ProductTypes.Any(x => x.ProductTypeId == product.ProductTypeId)){
                dbContext.Products.Add(product);
                dbContext.SaveChanges();
                return ResponseMessage.success;
            }return ResponseMessage.notfound;
        }

        public ResponseMessage DeleteProduct(int productId)
        {
            using (var trans = dbContext.Database.BeginTransaction())
            {
                if(dbContext.Products.Any(x =>x.ProductId  == productId))
                {
                    var prod = dbContext.Products.FirstOrDefault(x => x.ProductId == productId);
                    dbContext.Remove(prod);
                    dbContext.SaveChanges();
                    trans.Commit();
                    return ResponseMessage.success;
                }
                return ResponseMessage.notfound;
            }
        }


        public ResponseMessage UpdateProduct(Product product)
        {
            using (var tras = dbContext.Database.BeginTransaction())
            {
                if(dbContext.Products.Any(x => x.ProductId == product.ProductId))
                {
                    if(dbContext.ProductTypes.Any(x => x.ProductTypeId == product.ProductTypeId))
                    {
                        var pr = dbContext.Products.FirstOrDefault(x => x.ProductId == product.ProductId);
                        pr.ProductName = product.ProductName;
                        pr.ProductDescription = product.ProductDescription;
                        pr.ProductType = product.ProductType;
                        pr.ProductPrice = product.ProductPrice;
                        pr.ProductTypeId = product.ProductTypeId;
                        dbContext.SaveChanges();
                        tras.Commit();
                        return ResponseMessage.success;
                    }
                    return ResponseMessage.notfound;
                }
                return ResponseMessage.fail;
            }

        }


        public IEnumerable<Product> GetProducts(string? productName = null)
        {
            var result = dbContext.Products.Include(x => x.ProductType).AsQueryable();

            if(productName != null)
            {
                result = result.Where(x => x.ProductName.ToLower().Contains(productName.ToLower()));
            }
            return result;
        }

    }
}
