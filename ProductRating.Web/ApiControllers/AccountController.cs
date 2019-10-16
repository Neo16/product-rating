using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductRating.Bll.Dtos.Account;
using ProductRating.Bll.Dtos.Enum;
using ProductRating.Bll.ServiceInterfaces;
using ProductRating.Common;
using ProductRating.Dal.Model.Identity;
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

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid)
            {                
                return BadRequest("Mandatory fields should be filled.");
            }
            var passwordValidator = new PasswordValidator<ApplicationUser>();
            var passWordResult = await passwordValidator.ValidateAsync(userManager, null, model.Password);
            if (!passWordResult.Succeeded)
            {
                return BadRequest(passWordResult.Errors.First().Description);
            }          

            var dbUser = new ApplicationUser()
            {
                Email = model.Email,
                EmailConfirmed = true,
                Nationality = model.Natinonality,
                NickName = model.NickName,
                UserName = model.Email
            };
            IdentityResult result = await userManager.CreateAsync(dbUser);

            if (result.Succeeded)
            {
                switch (model.Role)
                {
                    case Role.Customer:
                        await userManager.AddToRoleAsync(dbUser, RoleNames.USER_ROLE);
                        break;
                    case Role.WebshopOwner:
                        await userManager.AddToRoleAsync(dbUser, RoleNames.SHOP_OWNER_ROLE);
                        break;
                }                
                await userManager.AddPasswordAsync(dbUser, model.Password);
                return Ok();
            }

            var errorMessage = result.Errors != null && result.Errors.Any()
                ? string.Join(',', result.Errors.Select(e => e.Description).ToList())
                : "Registration failed";
            return BadRequest(errorMessage);
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
                UserRoles = roles.ToList(),
                UserId = user.Id
            });
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto model)
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
