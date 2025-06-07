using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using asp_net_ecommerce_web_api.DTOs;
using asp_net_ecommerce_web_api.Models;

namespace asp_net_ecommerce_web_api.Controllers
{
    [ApiController]
    [Route("api/categories/")]
    public class CategoryController : ControllerBase
    {


        private static List<Category> categories = new List<Category>();

        // GET: /api/categories => Read categories
        [HttpGet]
        public IActionResult GetCategories()
        {
            var categoryList = categories.Select(c => new CategoryReadDto
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                Description = c.Description,
                CreatedAt = c.CreatedAt
            }).ToList();

            return Ok();
        }

        // POST: /api/categories => Create a category
        [HttpPost]
        public IActionResult CreateCategory([FromBody] CategoryCreateDto categoryData)
        {
            var newCategory = new Category
            {
                CategoryId = Guid.NewGuid(),
                Name = categoryData.Name,
                Description = categoryData.Description,
                CreatedAt = DateTime.UtcNow,
            };

            categories.Add(newCategory);

            var categoryReadDto = new CategoryReadDto
            {
                CategoryId = newCategory.CategoryId,
                Name = newCategory.Name,
                Description = newCategory.Description,
                CreatedAt = newCategory.CreatedAt,
            };

            return Created();
        }

        // PUT: /api/categories/{categoryId} => Update a category
        [HttpPut("{categoryId:guid}")]
        public IActionResult UpdateCategoryById(Guid categoryId, [FromBody] CategoryUpdateDto categoryData)
        {
            var foundCategory = categories.FirstOrDefault(category => category.CategoryId == categoryId);
            if (foundCategory == null)
            {
                return NotFound();
            }

            foundCategory.Name = categoryData.Name;
            foundCategory.Description = categoryData.Description;

            return Ok();
        }

        // DELETE: /api/categories/{categoryId} => Delete a category by Id
        [HttpDelete("{categoryId:guid}")]
        public IActionResult DeleteCategoryById(Guid categoryId)
        {
            var foundCategory = categories.FirstOrDefault(category => category.CategoryId == categoryId);
            if (foundCategory == null)
            {
                return NotFound();
            }
            categories.Remove(foundCategory);
            return Ok();
        }
    }
}