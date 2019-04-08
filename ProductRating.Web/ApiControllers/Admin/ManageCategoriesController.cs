using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductRating.Bll.Dtos.Category;
using ProductRating.Bll.ServiceInterfaces;
using ProductRating.Web.WebServices;
using System.Threading.Tasks;

namespace ProductRating.Web.ApiControllers.Admin
{
    [ApiController]
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

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto category)
        {
            await categoryService.CreateCategory(category);
            return Ok();
        }
    }
}
