using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using KAShop.Models;
namespace KAShop.Data
{
    public class ApplicationDbContext : DbContext   
        
    {
        public DbSet<Category> Categories { get; set; }


        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Server=db22947.public.databaseasp.net; Database=db22947; User Id=db22947; Password=6i_BT7y+#cR3; Encrypt=True; TrustServerCertificate=True; MultipleActiveResultSets=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // تحديد دقة وحجم لخاصية Price في Product
            // Explicitly set precision for Product.Price
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics" },
                new Category { Id = 2, Name = "Clothing" },
                new Category { Id = 3, Name = "Books" },
                new Category { Id = 4, Name = "Mobile" },
                new Category { Id = 5, Name = "Laptops" }

            );
        }
    }
}
