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
            Guid? userId = null;

            if (currentUserService.User != null)
            {
                userId = currentUserService.User.Id;
            }

            var productResult = await productService.GetDetails(productId, userId);
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

        [HttpGet("{productId}/score")]
        public async Task<IActionResult> Score(Guid productId)
        {
            var score = await reviewService.GetScoreOfProduct(productId);
            if (score == null)
            {
                return BadRequest("Unknown product.");
            }

            return Ok(score);
        }

        [HttpGet("{productId}/list-offes")]
        public async Task<IActionResult> ListOffers(Guid productId)
        {
            var offers = await productService.ListOffers(productId);
            return Ok(offers);
        }
    }
}
