using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProjectOgnenBozhinov5179.Controllers;
using ProjectOgnenBozhinov5179.Models;
using ProjectOgnenBozhinov5179.Models.DTOs;
using ProjectOgnenBozhinov5179.Services;
using Xunit;

namespace ProjectOgnenBozhinov5179.Tests.UnitTests.Controllers
{
    public class CategoriesControllerTests
    {
        private Mock<ICategoryService> _categoryServiceMock = new Mock<ICategoryService>();

        [Fact]
        public async Task GetCategories_ReturnsOkObjectResult()
        {
            // Arrange
            var categories = new List<Category> { new Category { Name = "Category1",Description = "Description" } };
            _categoryServiceMock.Setup(x => x.GetAllCategories()).ReturnsAsync(categories);
            var controller = new CategoriesController(_categoryServiceMock.Object);

            // Act
            var result = await controller.GetCategories() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(categories, result.Value);
        }

        [Fact]
        public async Task GetCategory_WithValidName_ReturnsOkObjectResult()
        {
            // Arrange
            var category = new Category { Name = "Category1",Description = "Description" };
            _categoryServiceMock.Setup(x => x.GetCategoryByName("Category1")).ReturnsAsync(category);
            var controller = new CategoriesController(_categoryServiceMock.Object);

            // Act
            var result = await controller.GetCategory("Category1") as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(category, result.Value);
        }

        [Fact]
        public async Task GetCategory_WithInvalidName_ReturnsNotFoundResult()
        {
            // Arrange
            _categoryServiceMock.Setup(x => x.GetCategoryByName("NonExistentCategory")).ReturnsAsync((Category)null);
            var controller = new CategoriesController(_categoryServiceMock.Object);

            // Act
            var result = await controller.GetCategory("NonExistentCategory") as NotFoundObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
            Assert.Equal("Category not found.", result.Value);
        }


        [Fact]
        public async Task CreateCategory_WithValidCategory_ReturnsOkResult()
        {
            // Arrange
            var category = new Category { Name = "NewCategory" };
            var controller = new CategoriesController(_categoryServiceMock.Object);

            // Act
            var result = await controller.CreateCategory(category) as OkResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task UpdateCategory_WithValidNameAndDto_ReturnsOkResult()
        {
            // Arrange
            var dto = new UpdateCategoryDto { Description = "Updated Description" };
            var controller = new CategoriesController(_categoryServiceMock.Object);

            // Act
            var result = await controller.UpdateCategory("CategoryToUpdate", dto) as OkResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task DeleteCategory_WithValidName_ReturnsOkResult()
        {
            // Arrange
            var controller = new CategoriesController(_categoryServiceMock.Object);

            // Act
            var result = await controller.DeleteCategory("CategoryToDelete") as OkResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }
    }
}
