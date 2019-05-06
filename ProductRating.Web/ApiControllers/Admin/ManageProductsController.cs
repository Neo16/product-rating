using Microsoft.AspNetCore.Mvc;
using ProductRating.Bll.Dtos.Product;
using ProductRating.Bll.ServiceInterfaces;
using ProductRating.Web.WebServices;
using System;
using System.Threading.Tasks;

namespace ProductRating.Web.ApiControllers.Admin
{
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
