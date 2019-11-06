using ProductRating.Dal.Model.Identity;
using System;
using System.Collections.Generic;

namespace ProductRating.Dal.Model.Entities.Products
{
    public class Category : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Category Parent { get; set; }

        public Guid? ParentId { get; set; }

        public ICollection<Category> Children { get; set; }

        public ICollection<Product> Products { get; set; }

        public ICollection<ProductAttribute> Attributes { get; set; }

        public Picture ThumbnailPicture { get; set; }

        public Guid? ThumbnailPictureId { get; set; }

        public Guid? CreatorId { get; set; }

        public ApplicationUser Creator { get; set; }
    }
}
