using System;
using System.Collections.Generic;

namespace ProductRating.Dal.Model.Entities.Reviews
{
    public class TextReview : ReviewBase, IEntity
    {
        public Guid Id { get; set; }

        public string Text { get; set; }       

        public ReviewMood Mood { get; set; }  

        public ICollection<ReviewVote> Votes { get; set; }

        public int Points { get; set; }
    }
}
