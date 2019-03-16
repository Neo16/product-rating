using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProductRating.Bll.ServiceInterfaces;
using ProductRating.Common;
using ProductRating.Dal;
using ProductRating.Model.Identity;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProductRating.Bll.Services
{
    public class TokenService : ServiceBase, ITokenService 
    {
        private readonly JwtSecurityTokenHandler accessTokenHandler;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly TokenConfiguration tokenConfiguration;


        public TokenService(
           ApplicationDbContext context,
           SignInManager<ApplicationUser> signInManager,
           UserManager<ApplicationUser> userManager,
           IOptions<TokenConfiguration> tokenConfiguration) : base(context)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.accessTokenHandler = new JwtSecurityTokenHandler
            {
                TokenLifetimeInMinutes = (int)(TimeSpan.FromDays(1)).TotalMinutes
            };
            this.tokenConfiguration = tokenConfiguration.Value;
        }

        public async Task<string> GetTokenForUserAsync(ApplicationUser user)
        {
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = "productraing.example",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfiguration.SigningKey)), SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity((await signInManager.CreateUserPrincipalAsync(user)).Claims),
            };

            var tokenHandler = accessTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            var accessToken = accessTokenHandler.WriteToken(tokenHandler);

            var res = await userManager.SetAuthenticationTokenAsync(user, "Local", "Access", accessToken);

            if (res.Succeeded)
            {
                return accessToken;
            }

            else
            {
                return null;
            }
        }
    }   
}
