using ProjectOgnenBozhinov5179.Models;

namespace ProjectOgnenBozhinov5179.Repositories
{
    public interface IProductsRepository
    {
        Task <List<Product>> GetAllProducts();
        Task <Product> GetProductById(int productId);
        Task CreateProduct( Product product);
        Task DeleteProduct(Product product);
        Task UpdateProduct( Product product );
        Task<List<Product>> GetProductsByCategory(string categoryName);
    }
}
