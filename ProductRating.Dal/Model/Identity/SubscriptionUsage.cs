using ProductRating.Dal.Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProductRating.Dal.Model.Identity
{
    public class SubscriptionUsage : IEntity
    {
        public Guid Id { get; set; }
        public long RequestCount { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public Subscription Subscription { get; set; }

        public Guid SubscriptionId { get; set; }
    }
}
