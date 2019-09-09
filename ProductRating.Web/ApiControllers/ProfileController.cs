using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductRating.Bll.Dtos;
using ProductRating.Bll.Dtos.Profile;
using ProductRating.Bll.ServiceInterfaces;
using ProductRating.Model.Identity;
using ProductRating.Web.WebServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductRating.Web.ApiControllers
{
    [Route("profile")]
    public class ProfileController : Controller
    {
        private readonly IProfileService profileService;
        private readonly IReviewService reviewService;
        private readonly CurrentUserService currentUserService;

        public ProfileController(
            CurrentUserService currentUserService,
            IProfileService profileService,
            IReviewService reviewService)
        {
            this.reviewService = reviewService;
            this.profileService = profileService;
            this.currentUserService = currentUserService;
        }

        [HttpGet("{userId?}")]
        [ProducesResponseType(typeof(ProfileDto), 200)]
        public async Task<IActionResult> Profile(Guid? userId)
        {
            var isMine = userId == null;
            if (isMine)
            {
                userId = (await currentUserService.GetCurrentUser()).Id;                
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
    }
}
