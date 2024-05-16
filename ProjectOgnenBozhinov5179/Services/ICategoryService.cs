using ProjectOgnenBozhinov5179.Models;
using ProjectOgnenBozhinov5179.Models.DTOs;

namespace ProjectOgnenBozhinov5179.Services
{
    public interface ICategoryService
    {
        Task CreateCategory(Category category);
        Task<List<Category>> GetAllCategories();
        Task<Category?> GetCategoryByName(string name);
        Task DeleteCategory(string name);
        Task UpdateCategory(string name, UpdateCategoryDto dto);
    }
}
