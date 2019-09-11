using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRating.Bll.Dtos.Review
{
    public class ScoreDto
    {
        public Guid ProductId { get; set; }
        public int Score { get; set; }
    }
}
