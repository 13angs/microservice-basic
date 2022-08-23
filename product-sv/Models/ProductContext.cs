using Microsoft.EntityFrameworkCore;

namespace product_sv.Models
{
    /// <summary>
    /// Represents a product.
    /// </summary>
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
            
        }

        public DbSet<Product> Products { get; private set; } = null!;
    }
}