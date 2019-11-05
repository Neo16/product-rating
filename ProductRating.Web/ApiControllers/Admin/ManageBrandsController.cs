using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductRating.Bll.Dtos;
using ProductRating.Bll.Dtos.Brand;
using ProductRating.Bll.ServiceInterfaces;
using ProductRating.Common;
using ProductRating.Web.WebServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductRating.Web.ApiControllers.Admin
{
    [ApiController]
    [Authorize(Roles = RoleNames.ADMIN_ROLE + "," + RoleNames.SHOP_OWNER_ROLE)]
    [Route("manage-brands")]
    public class ManageBrandsController : Controller
    {
        private readonly IBrandService brandService;
        private readonly CurrentUserService currentUserService;    

        public ManageBrandsController(
            IBrandService brandService,
            CurrentUserService currentUserService)
        {
            this.brandService = brandService;
            this.currentUserService = currentUserService;
        }
    
        [HttpPost("find")]
        [ProducesResponseType(typeof(List<BrandManageHeaderDto>), 200)]
        public async Task<IActionResult> ListBrands([FromBody] ManageBrandFilterDto filter, [FromQuery] PaginationDto pagination)
        {
            var brands = await brandService.AdminGetBrands(filter, currentUserService.User.Id, pagination);
            return Ok(brands);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrand(CreateEditBrandDto brand)
        {
            var creatorId = (await currentUserService.GetCurrentUser()).Id;
            var id =  await brandService.CreateBrand(brand, creatorId);
            return Ok(id);
        }

        [HttpGet("{brandId}/for-update")]
        public async Task<IActionResult> GetForUpdate(Guid brandId)
        {
            var brand = await brandService.GetBrandForUpdate(brandId);
            return Ok(brand);
        }

        [HttpPut("{brandId}")]
        public async Task<IActionResult> UpdateBrand(Guid brandId, CreateEditBrandDto brand)
        {
            await brandService.UpdateBrand(brandId, brand);
            return Ok();
        }

        [HttpDelete("{brandId}")]
        public async Task<IActionResult> DeleteBrand(Guid brandId)
        {
            await brandService.DeleteBrand(brandId);
            return Ok();
        }
    }
}
