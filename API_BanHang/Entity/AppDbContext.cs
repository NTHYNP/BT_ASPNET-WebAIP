using Microsoft.EntityFrameworkCore;

namespace API_BanHang.Entity
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<BillDetail> BillDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductType> ProductTypes { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server= DESKTOP-TD08TI7; Database = API_BanHang; Trusted_Connection = true; TrustServerCertificate = true");
        }

    }
}
