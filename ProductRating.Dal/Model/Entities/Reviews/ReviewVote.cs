using ProductRating.Dal.Model.Identity;
using System;

namespace ProductRating.Dal.Model.Entities.Reviews
{
    public class ReviewVote : IEntity
    {
        public Guid Id { get; set; }

        public ApplicationUser User { get; set; }

        public Guid UserId { get; set; }

        public VoteType VoteType { get; set; }

        public TextReview TextReview { get; set; }

        public Guid TextReviewId { get; set; }
    }
}
