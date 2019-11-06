using System;

namespace ProductRating.Bll.Dtos.Review
{
    public class TextReviewWithProductInfoDto
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public Guid ProductId { get; set; }

        public string ProductName { get; set; }

        public string ProductBrandName { get; set; }

        public string Date { get; set; }
    }
}
