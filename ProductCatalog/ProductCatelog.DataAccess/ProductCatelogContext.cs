namespace ProductCatalog.DataAccess
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;

    public class ProductCatalogContext : DbContext
    {
        public ProductCatalogContext() { }
        public ProductCatalogContext(DbContextOptions<ProductCatalogContext> options) : base(options)
        { }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasIndex(x => x.ProductName).IsUnique();
            modelBuilder.Entity<Product>()
                .Property(p => p.RowVersion).IsConcurrencyToken();
            modelBuilder.Entity<Product>().HasData(
                new { ProductId = 1, ProductName = "Sample Product", ProductPrice = 10, Image = Encoding.ASCII.GetBytes("Test"), LastUpdated = DateTime.Now });
        }
    }

    [Table("product")]
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name can't be longer than 50 characters")]
        public string ProductName { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please enter a valid integer value")]
        public int ProductPrice { get; set; }
        public DateTime LastUpdated { get; set; }
        public byte[] Image { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

    }
}