using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductRating.Bll.Dtos;
using ProductRating.Bll.Dtos.Product;
using ProductRating.Bll.ServiceInterfaces;
using ProductRating.Web.WebServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductRating.Web.ApiControllers.Admin
{
    [ApiController]
    [Authorize]
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

        [HttpPost("list")]
        [ProducesResponseType(typeof(List<ProductManageHeaderDto>), 200)]
        public async Task<IActionResult> ListProducts([FromBody] ManageProductFilterDto filter, [FromQuery] PaginationDto pagination)
        {
            var products = await productService.AdminGetProducts(filter, currentUserService.User.Id, pagination);
            return Ok(products);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateProduct(CreateEditProductDto product)
        {          
            await productService.CreateProduct(product);
            return Ok();
        }

        [HttpGet("get-for-update/{productId}")]
        public async Task<IActionResult> GetForUpdate(Guid productId)
        {
            var product = await productService.GetProductForUpdate(productId);
            return Ok(product);
        }

        [HttpPut("{productId}/update")]
        public async Task<IActionResult> UpdateProduct(Guid productId, CreateEditProductDto product)
        {
            await productService.UpdateProduct(productId, product);
            return Ok();
        }

        [HttpDelete("{productId}/delete")]
        public async Task<IActionResult> DeleteProduct(Guid productId)
        {
            await productService.DeleteProduct(productId);
            return Ok();
        }
    }
}
