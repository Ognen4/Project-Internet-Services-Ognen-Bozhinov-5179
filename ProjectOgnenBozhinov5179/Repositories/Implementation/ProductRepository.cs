using Microsoft.EntityFrameworkCore;
using ProjectOgnenBozhinov5179.Data;
using ProjectOgnenBozhinov5179.Models;

namespace ProjectOgnenBozhinov5179.Repositories.Implementation
{
    public class ProductRepository : IProductsRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllProducts()
        {
            var products = await _context.Products.Include(p=>p.Categories).ToListAsync();
            return products;
        }

        public async Task<Product> GetProductById(int productId)
        {
            var category = await _context.Products.Include(p => p.Categories).FirstOrDefaultAsync(p => p.Id == productId)
                ?? throw new Exception("Product not found.");

            return category;
        }

        public async Task UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Product>> GetProductsByCategory(string categoryName)
        {
            return await _context.Products.Include(p => p.Categories).Where(p => p.Categories.Any(c => c.Name == categoryName)).ToListAsync();
        }
    }
}
