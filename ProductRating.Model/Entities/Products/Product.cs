using ProductRating.Model.Entities.Reviews;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRating.Model.Entities.Products
{
    public class Product : IEntity
    {       
        public Guid Id { get; set; }

        public string Name { get; set; }
   
        public Brand Brand { get; set; }

        public Guid? BrandId { get; set; }

        public DateTime StartOfProduction { get; set; }
        public DateTime EndOfProduction { get; set; }
        public DateTime CreatedAt { get; set; }

        public Category Category { get; set; }

        public Guid CategoryId { get; set; }

        public ICollection<ProductAttributeValue> PropertyValues { get; set; }

        public ICollection<TextReview> Reviews { get; set; }

        public ICollection<Scorereview> Scores { get; set; }

        public double ScoreValue { get; set; }

        public Picture ThumbnailPicture { get; set; }

        public Guid? ThumbnailPictureId { get; set; }

        public ICollection<ProductPicture> Pictures { get; set; }
    }
}
