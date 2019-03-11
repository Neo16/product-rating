using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRating.Model.Entities.Products
{
    public abstract class ProductAttributeValue: IEntity
    {
        public Guid Id  { get; set; }

        public Product Product { get; set; }

        public Guid ProductId { get; set; }

        public ProductAttribute Attribute { get; set; }

        public Guid AttributeId { get; set; }      
    }
}
