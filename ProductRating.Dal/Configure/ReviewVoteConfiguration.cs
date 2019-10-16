using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductRating.Dal.Model.Entities.Reviews;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRating.Dal.Configure
{
    public class ReviewVoteConfiguration : IEntityTypeConfiguration<ReviewVote>
    {
        public void Configure(EntityTypeBuilder<ReviewVote> builder)
        {
            builder.HasOne(e => e.TextReview)
                .WithMany(e => e.Votes)
                .HasForeignKey(e => e.TextReviewId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
