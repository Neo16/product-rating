using System;
using System.Collections.Generic;

namespace ProductRating.Dal.Model.Entities.Products
{
    public abstract class ProductAttribute : IEntity
    {
        public Guid Id { get; set; }

        public Category Category { get; set; }

        public Guid CategoryId { get; set; }

        public string Name  { get; set; }

        public bool HasFixedValues  { get; set; }

        public ICollection<ProductAttributeValue> Values { get; set; }
    }
}
