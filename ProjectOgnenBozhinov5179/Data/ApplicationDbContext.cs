using Microsoft.EntityFrameworkCore;
using ProjectOgnenBozhinov5179.Models;

namespace ProjectOgnenBozhinov5179.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public virtual DbSet<Product>Products { get; set; }
        public virtual DbSet<Category>Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>(product =>
            {
                product.HasMany(p => p.Categories).WithMany();
            });
            modelBuilder.Entity<Category>(category =>
            {
                category.HasKey(p => p.Name);
            });
        }
    }
}
