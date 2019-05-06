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
        public async Task<IActionResult> ListCategories([FromBody] ManageProductFilterDto filter, [FromQuery] PaginationDto pagination)
        {
            var products = await productService.AdminGetProducts(filter, currentUserService.User.Id, pagination);
            return Ok(products);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Createproduct(CreateEditProductDto product)
        {
            await productService.CreateProduct(product);
            return Ok();
        }        

        [HttpPut("{productId}/update")]
        public async Task<IActionResult> Updateproduct(Guid productId, CreateEditProductDto product)
        {
            await productService.UpdateProduct(productId, product);
            return Ok();
        }

        [HttpDelete("{productId}/delete")]
        public async Task<IActionResult> Deleteproduct(Guid productId)
        {
            await productService.DeleteProduct(productId);
            return Ok();
        }
    }
}
