using ProductRating.Dal.Model.Identity;
using System.Threading.Tasks;

namespace ProductRating.Bll.ServiceInterfaces
{
    public interface ITokenService
    {
        Task<string> GetTokenForUserAsync(ApplicationUser user);
    }
}
