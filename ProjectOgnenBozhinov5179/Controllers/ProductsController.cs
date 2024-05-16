using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectOgnenBozhinov5179.Data;
using ProjectOgnenBozhinov5179.Models;
using ProjectOgnenBozhinov5179.Models.DTOs;
using ProjectOgnenBozhinov5179.Services;
using ProjectOgnenBozhinov5179.Services.Implementation;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectOgnenBozhinov5179.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productsService.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            try
            {
                var product = await _productsService.GetProductById(id);
                if (product == null)
                    return NotFound();
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDTO dto)
        {
            try
            {
                await _productsService.CreateProduct(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductDTO dto)
        {
            try
            {
                await _productsService.UpdateProduct(id, dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _productsService.DeleteProduct(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("calculateDiscount")]
        public async Task<IActionResult> CalculateDiscount([FromBody] List<int> productIds)
        {
            try
            {
                if (productIds == null || productIds.Count == 0)
                    return BadRequest("No product IDs provided for discount calculation.");

                decimal totalDiscount = 0;
                List<string> outOfStockProducts = new List<string>();
                List<int> processedProductIds = new List<int>();

                foreach (var productId in productIds)
                {
                    
                    if (processedProductIds.Contains(productId))
                        continue;

                    var productFromDb = await _productsService.GetProductById(productId);
                    if (productFromDb == null)
                        return NotFound($"Product with ID {productId} not found.");

                    var category = productFromDb.Categories.FirstOrDefault();
                    if (category == null)
                        return BadRequest($"Product with ID {productId} must have at least one category.");

                   
                    if (productFromDb.Quantity <= 0)
                    {
                        outOfStockProducts.Add($"Product with ID {productId} is out of stock.");
                        continue;
                    }

                    var productsInCategory = await _productsService.GetProductsByCategory(category.Name);
                    var isFirstProductInCategory = productsInCategory.Any(p => p.Id != productId);

                    if (productIds.Count > 1)
                    {
                        if (isFirstProductInCategory)
                        {
                            totalDiscount += productFromDb.Price * 0.05m;
                            processedProductIds.Add(productId);

                            var dto = new UpdateProductDTO
                            {
                                Name = productFromDb.Name,
                                Description = productFromDb.Description,
                                CategoryNames = productFromDb.Categories.Select(c => c.Name).ToList(),
                                Price = productFromDb.Price,
                                Quantity = productFromDb.Quantity - 1
                            };

                            await _productsService.UpdateProduct(productId, dto);
                        }
                    }
                    else
                    {
                        processedProductIds.Add(productId);
                    }
                }

                if (outOfStockProducts.Any())
                {
                    var outOfStockMessage = string.Join(", ", outOfStockProducts);
                    return BadRequest(outOfStockMessage);
                }

                return Ok(new { totalDiscount });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error calculating discount: {ex.Message}");
            }
        }

    }
}
