using ProductRating.Model.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductRating.Bll.ServiceInterfaces
{
    public interface ITokenService
    {
        Task<string> GetTokenForUserAsync(ApplicationUser user);
    }
}
