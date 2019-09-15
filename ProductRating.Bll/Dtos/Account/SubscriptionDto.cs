using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRating.Bll.Dtos.Account
{
    public class SubscriptionDto
    {
        public string Url { get; set; }

        public string Key { get; set; }

        public long DayLimit { get; set; }
    }
}
