using ProductRating.Model.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRating.Model.Entities.Reviews
{
    public class ReviewVote : IEntity
    {
        public Guid Id { get; set; }

        public ApplicationUser User { get; set; }

        public Guid UserId { get; set; }

        public VoteType MyProperty { get; set; }
    }
}
