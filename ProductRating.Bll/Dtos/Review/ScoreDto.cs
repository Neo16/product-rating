using System;

namespace ProductRating.Bll.Dtos.Review
{
    public class ScoreDto
    {
        public Guid ProductId { get; set; }
        public int Score { get; set; }
    }
}
