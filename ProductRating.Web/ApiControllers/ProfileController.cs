using Microsoft.AspNetCore.Mvc;
using ProductRating.Bll.Dtos;
using ProductRating.Bll.Dtos.Profile;
using ProductRating.Bll.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductRating.Web.ApiControllers
{
    [Route("profil")]
    public class ProfileController : Controller
    {
        private readonly IReviewService reviewService;

        public ProfileController(
            IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(ProfileDto), 200)]
        public async Task<IActionResult> Profile(Guid userId)
        {
            return Ok();
        }

        [HttpGet("my-profile")]     
        public async Task<IActionResult> MyProfile(Guid userId)
        {
            return Ok();
        }

        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateMyProfile(EditProfileDto profile)
        {
            return Ok();
        }
       

        [HttpGet("{userId}/reviews")]
        public async Task<IActionResult> Reviews(Guid userId)
        {
            var reviews = await reviewService.GetReviewsMadeByUser(userId);
            return Ok(reviews);
        }
    }
}
