using ProductRating.Dal.Model.Entities.Products;
using ProductRating.Dal.Model.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace ProductRating.Dal.Model.Entities.Reviews
{
    public abstract class ReviewBase
    {
   
        public Product Product { get; set; }

        public Guid ProductId { get; set; }
        
        public ApplicationUser Author { get; set; }

        public Guid AuthorId { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
