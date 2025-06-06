using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


using asp_net_ecommerce_web_api.Models;
using System.ComponentModel.DataAnnotations;

namespace asp_net_ecommerce_web_api.Controllers
{
    [ApiController]
    [Route("api/categories/")]
    public class CategoryController : ControllerBase
    {
        private static List<Category> categories = new List<Category>();

        [HttpGet]
        public IActionResult GetCategories([FromQuery] String searchValue)
        {
            if (searchValue != null)
            {
                var searchCategories = categories.Where(c => !string.IsNullOrEmpty(c.Name) && c.Name.Contains(searchValue, StringComparison.OrdinalIgnoreCase)).
                ToList();
                return Ok(searchCategories);
            }
            return Ok();
        }


        [HttpPost]
        public IActionResult CreateCategory([FromBody] Category categoryData)
        {
            if (string.IsNullOrEmpty(categoryData.Name))
            {
                return BadRequest("Category Name is Required and cannot be Empty");

            }
            if (categoryData.Name.Length < 2)
            {
                return BadRequest("Category Name size should be 2 characters Long");
            }
            var newCatagory = new Category
            {
                CategoryId = Guid.NewGuid(),
                Name = categoryData.Name,
                Description = categoryData.Description,
                CreatedAt = DateTime.UtcNow,
            };

            categories.Add(newCatagory);
            return Created($"/api/categories/{newCatagory.CategoryId}", newCatagory);
        }

        [HttpDelete("{categoryId:guid}")]
        public IActionResult DeleteCategorybyId(Guid categoryId)
        {
            var foundCategory = categories.FirstOrDefault(Category => Category.CategoryId == categoryId);

            if (foundCategory == null)
            {
                return NotFound("Category with this id does not exist");
            }
            categories.Remove(foundCategory);
            return NoContent();
        }
        [HttpPut("{categoryId:guid}")]
        public IActionResult UpdateCategorybyId(Guid categoryId, [FromBody] Category categoryData)
        {
            if (categoryData == null)
            {
                return BadRequest("Category Data is Missing");
            }
            var foundCategory = categories.FirstOrDefault(Category => Category.CategoryId == categoryId);
            if (foundCategory == null)
            {
                return NotFound("Category with this id does not exist");
            }
            if (!string.IsNullOrWhiteSpace(categoryData.Description))
            {
                foundCategory.Description = categoryData.Description;
            }
            return NoContent();
        }
    }

}