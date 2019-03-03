using ProductRating.Model.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using ProductRating.Model.Entities.Products;

namespace ProductRating.Model.Entities.Reviews
{
    public class TextReview : IEntity
    {
        public Guid Id { get; set; }

        public string Text { get; set; }       

        public ReviewMood Mood { get; set; }  

        public ICollection<ReviewVote> Votes { get; set; }

        public int Points { get; set; }
    }
}
