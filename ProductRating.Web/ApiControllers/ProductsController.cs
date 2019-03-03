using Microsoft.AspNetCore.Mvc;
using ProductRating.Bll.Dtos;
using ProductRating.Bll.Dtos.Product;
using ProductRating.Bll.ServiceInterfaces;
using System;
using System.Threading.Tasks;

namespace ProductRating.Web.ApiControllers
{
    [ApiController]
    [Route("products")]
    public class ProductsController : Controller
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet("find")]
        public async Task<IActionResult> Find(ProductFilterDto filter, PaginationDto pagination)
        {
            var productsResult = await productService.Find(filter, pagination);
            return Ok(productsResult);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> Details(Guid productId)
        {
            var productResult = await productService.GetDetails(productId);
            return Ok(productResult);
        }

    }
}
