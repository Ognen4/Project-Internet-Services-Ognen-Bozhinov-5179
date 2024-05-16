using System;
using Xunit;
using Moq;
using ProjectOgnenBozhinov5179.Services;
using ProjectOgnenBozhinov5179.Controllers;
using ProjectOgnenBozhinov5179.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectOgnenBozhinov5179.Models;

namespace ProjectOgnenBozhinov5179.Tests.UnitTests.Controllers
{
    public class ProductsControllerTests
    {
        [Fact]
        public async Task UpdateProduct_WithValidIdAndDto_ReturnsOkResult()
        {
            // Arrange
            var productServiceMock = new Mock<IProductsService>();
            var productsController = new ProductsController(productServiceMock.Object);
            var updateProductDto = new UpdateProductDTO { Name = "Name1",Description = "Description1", Price=100,Quantity = 2 };
            var productId = 1;

            productServiceMock.Setup(x => x.UpdateProduct(productId, updateProductDto))
                .Returns(Task.CompletedTask);

            // Act
            var result = await productsController.UpdateProduct(productId, updateProductDto);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeleteProduct_WithValidId_ReturnsOkResult()
        {
            // Arrange
            var productServiceMock = new Mock<IProductsService>();
            var productsController = new ProductsController(productServiceMock.Object);
            var productId = 1;

            productServiceMock.Setup(x => x.DeleteProduct(productId))
                .Returns(Task.CompletedTask);

            // Act
            var result = await productsController.DeleteProduct(productId);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task CalculateDiscount_WithValidProductIds_ReturnsOkResult()
        {
            var productServiceMock = new Mock<IProductsService>();
            var productsController = new ProductsController(productServiceMock.Object);
            var productIds = new List<int> { 1, 2 };

            productServiceMock.Setup(x => x.GetProductById(It.IsAny<int>()))
                .ReturnsAsync(new Product());

            productServiceMock.Setup(x => x.GetProductsByCategory(It.IsAny<string>()))
                .ReturnsAsync(new List<Product>());

            // Act
            var result = await productsController.CalculateDiscount(productIds);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.DoesNotContain("product IDs", badRequestResult.Value.ToString(), StringComparison.OrdinalIgnoreCase);
            Assert.DoesNotContain("discount calculation", badRequestResult.Value.ToString(), StringComparison.OrdinalIgnoreCase);
        }


        [Fact]
        public async Task GetProduct_WithValidId_ReturnsOkResult()
        {
            // Arrange
            var productServiceMock = new Mock<IProductsService>();
            var productsController = new ProductsController(productServiceMock.Object);
            var productId = 1;

            productServiceMock.Setup(x => x.GetProductById(productId))
                .ReturnsAsync(new Product());

            // Act
            var result = await productsController.GetProduct(productId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
