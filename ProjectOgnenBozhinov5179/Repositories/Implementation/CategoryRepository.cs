using Microsoft.EntityFrameworkCore;
using ProjectOgnenBozhinov5179.Data;
using ProjectOgnenBozhinov5179.Models;

namespace ProjectOgnenBozhinov5179.Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategory(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Category>> GetAllCategories()
        {
            var categories = await _context.Categories.ToListAsync();

            return categories;
        }

        public async Task<Category?> GetCategoryByName(string name)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == name);
            return category;
        }

        public async Task UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }
    }
}
