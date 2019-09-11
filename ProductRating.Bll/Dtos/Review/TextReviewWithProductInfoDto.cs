using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRating.Bll.Dtos.Review
{
    public class TextReviewWithProductInfoDto 
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public string ProductName { get; set; }

        public string ProductBrandName { get; set; }

        public string ProductCategoryName { get; set; }

    }
}
