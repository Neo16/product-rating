using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRating.Bll.Dtos.Account
{
    public class RequireSubscriptionDto
    {
        public string Url { get; set; }

        public long DayLimit { get; set; }
    }
}
