using Microsoft.EntityFrameworkCore;
using ProductRating.Dal;
using System.Threading.Tasks;

namespace ProductRating.Bll.Services
{
    public class ApiKeyService : ServiceBase, IApiKeyService
    {
        public ApiKeyService(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> IsApiKeyValid(string key)
        {
            return await context.Users.AnyAsync(e => e.ApiKey == key);
        }
    }
}
