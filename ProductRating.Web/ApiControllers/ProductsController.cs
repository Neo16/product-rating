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
        private readonly IReviewService reviewService;

        public ProductsController(
            IProductService productService,
            IReviewService reviewService)
        {
            this.productService = productService;
            this.reviewService = reviewService;
        }

        [HttpGet("find")]
        public async Task<IActionResult> Find([FromBody] ProductFilterDto filter, [FromQuery] PaginationDto pagination)
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

        [HttpGet("{productId}/reviews")]
        public async Task<IActionResult> Reviews(Guid productId)
        {
            var reviews = await reviewService.GetReviewsOfProduct(productId);
            return Ok(reviews);
        }
    }
}
