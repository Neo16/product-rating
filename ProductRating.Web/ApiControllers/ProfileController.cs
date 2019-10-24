using Microsoft.AspNetCore.Mvc;
using ProductRating.Bll.Dtos.Account;
using ProductRating.Bll.Dtos.Profile;
using ProductRating.Bll.ServiceInterfaces;
using ProductRating.Web.WebServices;
using System;
using System.Threading.Tasks;

namespace ProductRating.Web.ApiControllers
{
    [Route("profile")]
    public class ProfileController : Controller
    {
        private readonly IProfileService profileService;
        private readonly IReviewService reviewService;
        private readonly CurrentUserService currentUserService;
        private readonly ISubscriptionService subscriptionService;

        public ProfileController(
            CurrentUserService currentUserService,
            IProfileService profileService,
            IReviewService reviewService,
            ISubscriptionService apiKeyService)
        {
            this.reviewService = reviewService;
            this.profileService = profileService;
            this.currentUserService = currentUserService;
            this.subscriptionService = apiKeyService;
        }

        [HttpGet("{userId?}")]
        [ProducesResponseType(typeof(ProfileDto), 200)]
        public async Task<IActionResult> Profile(Guid? userId)
        {
            var isMine = userId == null;
            if (isMine)
            {
                userId = currentUserService.User.Id;                
            }

            var profile = await profileService.GetProfileByUserId(userId.Value);
            if (profile == null)
            {
                return BadRequest();
            }
            profile.IsMine = isMine;

            return Ok(profile);
        }
       

        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateMyProfile([FromBody] EditProfileDto profile)
        {
            if (ModelState.IsValid)
            {
                await profileService.UpdateProfile(currentUserService.User.Id, profile);
                return Ok();
            }
            return BadRequest();
        }
       

        [HttpGet("{userId}/reviews")]
        public async Task<IActionResult> Reviews(Guid userId)
        {
            var reviews = await reviewService.GetReviewsMadeByUser(userId);
            return Ok(reviews);
        }

        [HttpGet("subscriptions")]
        public async Task<IActionResult> GetSubscriptions()
        {
            var subscriptions = await subscriptionService.GetSubscriptions(currentUserService.User.Id);
            return Ok(subscriptions);
        }

        [HttpPost("require-subscription")]
        public async Task<IActionResult> RequireSubscription([FromBody] RequireSubscriptionDto requireApiKeyDto)
        {
            await subscriptionService.RequireSubscription(currentUserService.User.Id, requireApiKeyDto);
            return Ok();
        }

        [HttpDelete("subscriptions/{subscriptionId}")]
        public async Task<IActionResult> DeleteSubscription(Guid subscriptionId)
        {
            await subscriptionService.DeleteSubscription(subscriptionId);
            return Ok();
        }
    }
}
