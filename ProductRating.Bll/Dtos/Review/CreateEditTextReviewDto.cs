using ProductRating.Model.Entities.Reviews;
using System;

namespace ProductRating.Bll.Dtos.Review
{
    public class CreateEditTextReviewDto
    {
        public ReviewMood Mood { get; set; }

        public string Text { get; set; }

        public Guid? ProductId { get; set; }
    }
}
