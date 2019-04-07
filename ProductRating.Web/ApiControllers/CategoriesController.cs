using Microsoft.AspNetCore.Mvc;
using ProductRating.Bll.Dtos.Category;
using ProductRating.Bll.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductRating.Web.ApiControllers
{
    [ApiController]
    [Route("categories")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(List<CategoryHeaderDto>), 200)]
        public async Task<IActionResult> GetMainCategories()
        {
            var categories = await categoryService.GetMainCategories();
            return  Ok(categories);
        }

        [HttpGet("{categoryId}/subcategories")]
        [ProducesResponseType(typeof(List<CategoryHeaderDto>), 200)]
        public async Task<IActionResult> GetChildCategoriesOf(Guid categoryId)
        {
            var categories = await categoryService.GetChildCategoriesOf(categoryId);
            return Ok(categories);
        }

        [HttpGet("{categoryId}/attributes")]
        [ProducesResponseType(typeof(List<CategoryHeaderDto>), 200)]
        public async Task<IActionResult> GetAttributesOf(Guid categoryId)
        {
            var attributes = await categoryService.GetAttributesOf(categoryId);
            return Ok(attributes);
        }
    }
}
