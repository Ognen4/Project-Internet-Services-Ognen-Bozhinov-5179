using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectOgnenBozhinov5179.Data;
using ProjectOgnenBozhinov5179.Models;
using ProjectOgnenBozhinov5179.Models.DTOs;
using ProjectOgnenBozhinov5179.Services;
using System;
using System.Linq;

namespace ProjectOgnenBozhinov5179.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryService.GetAllCategories();
            return Ok(categories);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetCategory(string name)
        {
            try
            {
                var category = await _categoryService.GetCategoryByName(name);
                if (category == null)
                    return NotFound("Category not found.");
                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            try
            {
                await _categoryService.CreateCategory(category);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{name}")]
        public async Task<IActionResult> UpdateCategory(string name, UpdateCategoryDto dto)
        {
            try
            {
                await _categoryService.UpdateCategory(name, dto);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteCategory(string name)
        {
            try
            {
                await _categoryService.DeleteCategory(name);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
