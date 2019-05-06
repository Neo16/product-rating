using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductRating.Bll.Dtos;
using ProductRating.Bll.Dtos.Category;
using ProductRating.Bll.ServiceInterfaces;
using ProductRating.Web.WebServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductRating.Web.ApiControllers.Admin
{
    [ApiController]
    [Authorize]
    [Route("manage-categories")]
    public class ManageCategoriesController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly CurrentUserService currentUserService;    

        public ManageCategoriesController(
            ICategoryService categoryService,
            CurrentUserService currentUserService)
        {
            this.categoryService = categoryService;
            this.currentUserService = currentUserService;
        }

        [HttpPost("list")]
        [ProducesResponseType(typeof(List<CategoryManageHeaderDto>), 200)]
        public async Task<IActionResult> ListCategories([FromBody] ManageCategoryFilterDto filter, [FromQuery] PaginationDto pagination)
        {
            var categories = await categoryService.GetCategories(filter, currentUserService.User.Id, pagination);
            return Ok(categories);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCategory(CreateEditCategoryDto category)
        {
            await categoryService.CreateCategory(category);
            return Ok();
        }

        [HttpGet("get-for-update")]
        public async Task<IActionResult> UpdateCategory(Guid categoryId)
        {
            var category = await categoryService.GetCategoryForUpdate(categoryId);
            return Ok(category);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateCategory(Guid categoryId, CreateEditCategoryDto category)
        {
            await categoryService.UpdateCategory(categoryId, category);
            return Ok();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteCategory(Guid categoryId)
        {
            await categoryService.DeleteCategory(categoryId);
            return Ok();
        }
    }
}
