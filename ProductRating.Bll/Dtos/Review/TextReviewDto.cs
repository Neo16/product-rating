using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRating.Bll.Dtos.Review
{
    public class TextReviewDto
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public bool IsMine { get; set; }

        public bool WasUpvotedByMe { get; set; }

        public bool WasDownvotedByMe { get; set; }
    }
}
