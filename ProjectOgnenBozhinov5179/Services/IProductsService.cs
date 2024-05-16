
using ProjectOgnenBozhinov5179.Models;
using ProjectOgnenBozhinov5179.Models.DTOs;

namespace ProjectOgnenBozhinov5179.Services
{
    public interface IProductsService
    {
        Task <List<Product>> GetAllProducts();
        Task <Product> GetProductById(int productId);
        Task CreateProduct(CreateProductDTO dto);
        Task DeleteProduct(int productId);
        Task UpdateProduct(int id,UpdateProductDTO dto);
        Task<List<Product>> GetProductsByCategory(string categoryName);
    }
}
