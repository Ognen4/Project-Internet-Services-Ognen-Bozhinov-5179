using ProjectOgnenBozhinov5179.Models;
using ProjectOgnenBozhinov5179.Models.DTOs;
using ProjectOgnenBozhinov5179.Repositories;

namespace ProjectOgnenBozhinov5179.Services.Implementation
{
    public class ProductService : IProductsService
    {
        private readonly IProductsRepository _productRepository;
        private readonly ICategoryService _categoryService;

        public ProductService(IProductsRepository productRepository, ICategoryService categoryService)
        {
            _productRepository = productRepository;
            _categoryService = categoryService;
        }

        public async Task CreateProduct(CreateProductDTO dto)
        {
            var categories = new List<Category>();
            foreach(var categoryName in dto.CategoryNames)
            {
                var category = await _categoryService.GetCategoryByName(categoryName);
                if (category == null)
                {
                    category = new Category { Name = categoryName };
                    await _categoryService.CreateCategory(category);
                }
                categories.Add(category);

            }
            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Quantity = dto.Quantity,
                Categories = categories
            };
            await _productRepository.CreateProduct(product);
        }

        public async Task DeleteProduct(int id)
        {
            var product = await GetProductById(id);
            await _productRepository.DeleteProduct(product);
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _productRepository.GetAllProducts();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _productRepository.GetProductById(id);
        }

        public async Task UpdateProduct(int id, UpdateProductDTO dto)
        {
            var product = await GetProductById(id);
            var categories = new List<Category>();
            foreach (var categoryName in dto.CategoryNames)
            {
                var category = await _categoryService.GetCategoryByName(categoryName);
                if (category == null)
                {
                    category = new Category { Name = categoryName };
                    await _categoryService.CreateCategory(category);
                }
                categories.Add(category);
            }
            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;
            product.Quantity = dto.Quantity;
            product.Categories = categories;
            await _productRepository.UpdateProduct(product);
        }

        public async Task<List<Product>> GetProductsByCategory(string categoryName)
        {
            return await _productRepository.GetProductsByCategory(categoryName);
        }
    }
}
