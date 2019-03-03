using ProductRating.Model.Identity;
using System;

namespace ProductRating.Model.Entities.Reviews
{
    public class ReviewVote : IEntity
    {
        public Guid Id { get; set; }

        public ApplicationUser User { get; set; }

        public Guid UserId { get; set; }

        public VoteType VoteType { get; set; }
    }
}
