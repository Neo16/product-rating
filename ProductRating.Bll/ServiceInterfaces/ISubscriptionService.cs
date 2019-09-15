using ProductRating.Bll.Dtos.Account;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductRating.Bll.ServiceInterfaces
{
    public interface ISubscriptionService
    {
        Task<bool> IsApiKeyValid(string siteBaseUrl, string key);

        Task RequireSubscription(Guid userId, RequireSubscriptionDto apiKey);

        Task<List<SubscriptionDto>> GetSubscriptions(Guid userId);

        Task DeleteSubscription(Guid keyId);
    }
}
