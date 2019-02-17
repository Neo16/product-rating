using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRating.Model.Entities.Product
{
    public abstract class ProductPropertyValue : IEntity
    {
        public Guid Id  { get; set; }

        public Product Product { get; set; }

        public Guid ProductId { get; set; }

        public ProductProperty ProductPropertyType { get; set; }

        public Guid ProductPropertyTypeId { get; set; }
    }
}
