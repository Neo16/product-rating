using System;

namespace ProductRating.Bll.Dtos.Account
{
    public class SubscriptionDto
    {
        public Guid Id { get; set; }

        public string Url { get; set; }

        public string Key { get; set; }

        public long DayLimit { get; set; }
    }
}
