using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductRating.Bll.Dtos.Account;
using ProductRating.Bll.ServiceInterfaces;
using ProductRating.Model.Identity;
using ProductRating.Web.WebServices;
using System.Linq;
using System.Threading.Tasks;

namespace ProductRating.Web.ApiControllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ITokenService tokenService;
        private readonly CurrentUserService currentUserService;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            ITokenService tokenService,
            CurrentUserService currentUserService)
        {
            this.userManager = userManager;
            this.tokenService = tokenService;
            this.currentUserService = currentUserService;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResultDto), 200)]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            //todo elszáll ha nincs ilyen user. 
            var user = await userManager.FindByNameAsync(model.UserName);

            if (user == null || !await userManager.CheckPasswordAsync(user, model.Password))
            {
                return BadRequest("Invalid username or password.");
            }
            var token = await tokenService.GetTokenForUserAsync(user);

            var roles = await userManager.GetRolesAsync(user);

            return Ok(new LoginResultDto()
            {
                UserToken = token,
                UserName = user.UserName,
                UserRoles = roles.ToList()
            });
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto model)
        {
            ApplicationUser user = await currentUserService.GetCurrentUser();
            IdentityResult result = await userManager.ChangePasswordAsync(user, model.Password, model.NewPassword);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Errors);
            }           
        }       
    }
}
