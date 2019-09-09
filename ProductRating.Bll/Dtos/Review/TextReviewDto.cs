using ProductRating.Model.Entities.Reviews;
using System;

namespace ProductRating.Bll.Dtos.Review
{
    public class TextReviewDto
    {
        public Guid Id { get; set; }

        public string AuthorName { get; set; }

        public string CreatedAt { get; set; }

        public string Text { get; set; }

        public bool IsMine { get; set; }

        public bool WasUpvotedByMe { get; set; }

        public bool WasDownvotedByMe { get; set; }

        public ReviewMood Mood { get; set; }

        public int Points { get; set; }
    }
}
