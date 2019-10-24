using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductRating.Bll.Dtos;
using ProductRating.Bll.Dtos.Product;
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
    [Route("manage-products")]
    public class ManageProductsController : Controller
    {
        private readonly IProductService productService;
        private readonly CurrentUserService currentUserService;

        public ManageProductsController(
            IProductService productService,
            CurrentUserService currentUserService)
        {
            this.productService = productService;
            this.currentUserService = currentUserService;
        }

        [HttpPost("find")]
        [ProducesResponseType(typeof(List<ProductManageHeaderDto>), 200)]
        public async Task<IActionResult> ListProducts([FromBody] ManageProductFilterDto filter, [FromQuery] PaginationDto pagination)
        {
            var products = await productService.AdminGetProducts(filter, currentUserService.User.Id, pagination);
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateEditProductDto product)
        {
            await productService.CreateProduct(product);
            return Ok();
        }

        [HttpGet("{productId}/offer")]
        public async Task<IActionResult> GetOffer(Guid productId)
        {
            var offer = await productService.GetOfferForProduct(currentUserService.User.Id, productId);
            return Ok(offer);
        }


        [HttpPost("{productId}/offer")]
        public async Task<IActionResult> AddOffer(Guid productId, [FromBody] CreateEditOfferDto offer)
        {
            await productService.AddOffer(currentUserService.User.Id, productId, offer);
            return Ok();

        }

        [HttpDelete("{productId}/offer")]
        public async Task<IActionResult> DeletedOffer(Guid productId)
        {
            await productService.DeleteOffer(currentUserService.User.Id, productId);
            return Ok();
        }


        [HttpGet("{productId}/for-update")]
        public async Task<IActionResult> GetForUpdate(Guid productId)
        {
            var product = await productService.GetProductForUpdate(productId);
            return Ok(product);
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProduct(Guid productId, CreateEditProductDto product)
        {
            await productService.UpdateProduct(productId, product, currentUserService.User.Id, User.IsInRole(RoleNames.ADMIN_ROLE));
            return Ok();
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct(Guid productId)
        {
            await productService.DeleteProduct(productId, currentUserService.User.Id, User.IsInRole(RoleNames.ADMIN_ROLE));
            return Ok();
        }    
    }
}
