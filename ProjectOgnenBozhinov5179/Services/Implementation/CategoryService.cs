using ProjectOgnenBozhinov5179.Models;
using ProjectOgnenBozhinov5179.Models.DTOs;
using ProjectOgnenBozhinov5179.Repositories;

namespace ProjectOgnenBozhinov5179.Services.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task CreateCategory(Category category)
        {
            await _categoryRepository.CreateCategory(category);
        }

        public async Task DeleteCategory(string name)
        {
            var category = await GetCategoryByName(name)
            ?? throw new Exception("Category not found.");
            await _categoryRepository.DeleteCategory(category);
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _categoryRepository.GetAllCategories();
        }

        public async Task<Category?> GetCategoryByName(string name)
        {
            return await _categoryRepository.GetCategoryByName(name);
        }

        public async Task UpdateCategory(string name, UpdateCategoryDto dto)
        {
            var category = await GetCategoryByName(name);

            category.Description = dto.Description;

            await _categoryRepository.UpdateCategory(category);
        }
    }
}
