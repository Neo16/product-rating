using Microsoft.EntityFrameworkCore;
using ProductRating.Dal;
using ProductRating.Model.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProductRating.Bll.Services
{
    public class ApiKeyService : ServiceBase, IApiKeyService
    {
        public ApiKeyService(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> IsApiKeyValid(string siteBaseUrl, string key)
        {
            var subscription = await context.Subscriptions
                .Include(s => s.Usage)
                .FirstOrDefaultAsync(s => s.ApiKey == key && s.SiteBaseUrl == siteBaseUrl);

            if (subscription != null)
            {
                var todaysUsage = subscription.Usage.Where(e => e.Date == DateTime.Today).SingleOrDefault();

                if (todaysUsage == null)
                {
                    todaysUsage = new SubscriptionUsage() { Date = DateTime.Today, RequestCount = 1 };
                    subscription.Usage.Add(todaysUsage);
                }
                else
                {
                    todaysUsage.RequestCount++;
                }
                await context.SaveChangesAsync();

                return !(todaysUsage.RequestCount > subscription.DayLimit);
            }
            else
            {
                return false;
            }
        }
    }
}
