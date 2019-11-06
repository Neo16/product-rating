using ProductRating.Dal.Model.Entities;
using System;
using System.Collections.Generic;

namespace ProductRating.Dal.Model.Identity
{
    public class Subscription : IEntity
    {
        public Guid Id { get; set; }

        public string ApiKey { get; set; }

        public string SiteBaseUrl { get; set; }

        public ApplicationUser User { get; set; }

        public Guid UserId { get; set; }

        /// <summary>
        /// Max number of requests/day with this subscription 
        /// </summary>
        public long DayLimit { get; set; }

        /// <summary>
        /// Log of requests 
        /// </summary>
        public virtual ICollection<SubscriptionUsage> Usage { get; set; } = new List<SubscriptionUsage>();

    }
}
