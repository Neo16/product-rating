using Microsoft.EntityFrameworkCore;
using ProductRating.Bll.Dtos.Account;
using ProductRating.Bll.ServiceInterfaces;
using ProductRating.Dal;
using ProductRating.Model.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductRating.Bll.Services
{
    public class SubscriptionService : ServiceBase, ISubscriptionService
    {
        public SubscriptionService(ApplicationDbContext context) : base(context)
        {
        }

        public async Task RequireSubscription(Guid userId, RequireSubscriptionDto request)
        {
            var dbSubscription = new Subscription()
            {
                ApiKey = Guid.NewGuid().ToString(),
                DayLimit = request.DayLimit,
                SiteBaseUrl = request.Url,
                UserId = userId
            };

            context.Subscriptions.Add(dbSubscription);
            await context.SaveChangesAsync();
        }

        public async Task DeleteSubscription(Guid keyId)
        {
            var dbSubscription = await context.Subscriptions
                .FirstOrDefaultAsync(e => e.Id == keyId);

            if (dbSubscription != null)
            {
                context.Subscriptions.Remove(dbSubscription);
            }
            await context.SaveChangesAsync();
        }

        public async Task<List<SubscriptionDto>> GetSubscriptions(Guid userId)
        {
           var result = await context.Subscriptions
                .Where(e => e.UserId == userId)
                .Select(e => new SubscriptionDto()
                {
                    Key = e.ApiKey,
                    Url  = e.SiteBaseUrl,
                    DayLimit = e.DayLimit                    
                })
                .ToListAsync();

            return result;
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
