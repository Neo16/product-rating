using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductRating.Bll.Dtos;
using ProductRating.Bll.Dtos.Category;
using ProductRating.Bll.ServiceInterfaces;
using ProductRating.Common;
using ProductRating.Web.WebServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductRating.Web.ApiControllers.Admin
{
    [ApiController]
    [Route("manage-categories")]
    [Authorize(Roles = RoleNames.ADMIN_ROLE + "," + RoleNames.SHOP_OWNER_ROLE)]
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

        [HttpPost("find")]
        [ProducesResponseType(typeof(List<CategoryManageHeaderDto>), 200)]
        public async Task<IActionResult> ListCategories([FromBody] ManageCategoryFilterDto filter, [FromQuery] PaginationDto pagination)
        {
            var categories = await categoryService.AdminGetCategories(filter, currentUserService.User.Id, pagination);
            return Ok(categories);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateEditCategoryDto category)
        {
            var id = await categoryService.CreateCategory(category);
            return Ok(id);
        }

        [HttpGet("{categoryId}/for-update")]
        public async Task<IActionResult> GetForUpdate(Guid categoryId)
        {
            var category = await categoryService.GetCategoryForUpdate(categoryId);
            return Ok(category);
        }

        [HttpPut("{categoryId}")]
        public async Task<IActionResult> UpdateCategory(Guid categoryId, CreateEditCategoryDto category)
        {
            await categoryService.UpdateCategory(categoryId, category);
            return Ok();
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategory(Guid categoryId)
        {
            await categoryService.DeleteCategory(categoryId);
            return Ok();
        }
    }
}
