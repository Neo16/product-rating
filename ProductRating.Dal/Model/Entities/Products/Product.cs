using ProductRating.Dal.Model.Entities.Reviews;
using ProductRating.Dal.Model.Identity;
using System;
using System.Collections.Generic;

namespace ProductRating.Dal.Model.Entities.Products
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

        public ICollection<ProductAttributeValueConnection> PropertyValueConnections { get; set; }

        public ICollection<TextReview> Reviews { get; set; }

        public ICollection<Scorereview> Scores { get; set; }

        public double ScoreValue { get; set; }

        public Picture ThumbnailPicture { get; set; }

        public Guid? ThumbnailPictureId { get; set; }

        public ICollection<ProductPicture> Pictures { get; set; }

        public Guid? CreatorId { get; set; }

        public ApplicationUser Creator { get; set; }

        public virtual ICollection<Offer> Offers { get; set; }

        public long? SmallestPrice { get; set; }
    }
}
