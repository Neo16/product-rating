using Microsoft.AspNetCore.Mvc;
using ProductRating.Bll.Dtos;
using ProductRating.Bll.Dtos.Product;
using ProductRating.Bll.ServiceInterfaces;
using ProductRating.Web.WebServices;
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
        private readonly CurrentUserService currentUserService;

        public ProductsController(
            IProductService productService,
            IReviewService reviewService,
            CurrentUserService currentUserService)
        {
            this.productService = productService;
            this.reviewService = reviewService;
            this.currentUserService = currentUserService;
        }

        [HttpPost("find")]
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
            Guid? userId = null;

            if (currentUserService.User != null)
            {
                userId = currentUserService.User.Id;
            }

            var reviews = await reviewService.GetReviewsOfProduct(userId, productId);
            return Ok(reviews);
        }
    }
}
