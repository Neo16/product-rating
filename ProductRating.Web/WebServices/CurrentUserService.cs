using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ProductRating.Dal.Model.Identity;
using System.Threading.Tasks;

namespace ProductRating.Web.WebServices
{
    public class CurrentUserService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<ApplicationUser> userManager;
        public ApplicationUser User { get; }

        public CurrentUserService(
            IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;

            this.User = GetCurrentUser().Result;
        }

        public async Task<ApplicationUser> GetCurrentUser()
        {
            var userClaimsPrincipal = httpContextAccessor.HttpContext.User;
            return await userManager.GetUserAsync(userClaimsPrincipal);
        }
    }
}
