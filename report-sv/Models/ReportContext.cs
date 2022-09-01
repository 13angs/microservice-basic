using Microsoft.EntityFrameworkCore;

namespace report_sv.Models {   
    public class ReportContext : DbContext
    {
        public ReportContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Product> Products { get; private set; } = null!;
        public DbSet<Order> Orders { get; private set; } = null!;
    }
}