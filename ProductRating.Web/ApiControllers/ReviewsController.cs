using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductRating.Bll.Dtos;
using ProductRating.Bll.Dtos.Review;
using ProductRating.Bll.ServiceInterfaces;
using ProductRating.Common;
using ProductRating.Web.WebServices;
using System;
using System.Threading.Tasks;

namespace ProductRating.Web.ApiControllers
{
    [ApiController]
    [Route("reviews")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ReviewsController : Controller
    {
        private readonly IReviewService reviewService;
        private readonly CurrentUserService currentUserService;

        public ReviewsController(
            IReviewService reviewService,
            CurrentUserService currentUserService)
        {
            this.reviewService = reviewService;
            this.currentUserService = currentUserService;
        }

        [HttpPost("add-scrore")]
        public async Task<IActionResult> AddScoreReview(ScoreDto scoreDto)
        {
            await reviewService.AddScore(currentUserService.User.Id, scoreDto);
            return Ok();
        }

        [HttpPost("add-review")]
        public async Task<IActionResult> AddTextReview(CreateEditTextReviewDto textReview)
        {
            if (textReview.ProductId == null)
            {
                return BadRequest();
            }
            var review = await reviewService.AddReview(currentUserService.User.Id, textReview);
            return Ok(review);
        }

        [HttpPut("{reviewId}/update")]
        public async Task<IActionResult> UpdateTextReview(Guid reviewId, CreateEditTextReviewDto textReview)
        {
            await reviewService.UpdateReview(currentUserService.User.Id, reviewId, textReview);
            return Ok();
        }

        [HttpDelete("{reviewId}")]
        public async Task<IActionResult> DeleteTextReview(Guid reviewId)
        {
            await reviewService.DeleteReview(currentUserService.User.Id, reviewId, User.IsInRole(RoleNames.ADMIN_ROLE));
            return Ok();
        }

        [HttpPost("upvote-review")]
        public async Task<IActionResult> UpVoteTextReview([FromBody] IdDto reviewId)
        {
            await reviewService.UpvoteReview(currentUserService.User.Id, reviewId.Id);
            return Ok();
        }

        [HttpPost("downvote-review")]
        public async Task<IActionResult> DownVoteTextReview([FromBody] IdDto reviewId)
        {
            await reviewService.DownVoteReview(currentUserService.User.Id, reviewId.Id);
            return Ok();
        }      
    }
}
