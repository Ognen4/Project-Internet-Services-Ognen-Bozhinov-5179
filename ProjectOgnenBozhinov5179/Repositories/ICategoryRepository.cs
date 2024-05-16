using ProjectOgnenBozhinov5179.Models;

namespace ProjectOgnenBozhinov5179.Repositories
{
    public interface ICategoryRepository
    {
        Task CreateCategory(Category category);
        Task<List<Category>> GetAllCategories();
        Task<Category?> GetCategoryByName(string name);
        Task DeleteCategory(Category category);
        Task UpdateCategory(Category category);
    }
}
