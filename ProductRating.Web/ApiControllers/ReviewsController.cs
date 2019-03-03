using Microsoft.AspNetCore.Mvc;
using ProductRating.Bll.Dtos.Review;
using ProductRating.Bll.ServiceInterfaces;
using System;
using System.Threading.Tasks;

namespace ProductRating.Web.ApiControllers
{
    [ApiController]
    [Route("reviews")]
    public class ReviewsController : Controller
    {
        private readonly IReviewService reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }
 
        [HttpPost("add")]
        public async Task<IActionResult> AddTextReview(CreateEditTextReviewDto textReview)
        {
            await reviewService.Add(textReview);
            return Ok();
        }

        [HttpPut("{reviewId}/update")]
        public async Task<IActionResult> UpdateTextReview(Guid reviewId, CreateEditTextReviewDto textReview)
        {
            await reviewService.Update(reviewId, textReview);
            return Ok();
        }

        [HttpDelete("{reviewId}")]
        public async Task<IActionResult> DeleteTextReview(Guid reviewId)
        {
            await reviewService.Delete(reviewId);
            return Ok();
        }      
        

        //GetForUser() -  az összes review amit a user adott (bejelentkezés kell)
    }
}
