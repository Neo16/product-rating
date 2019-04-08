using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRating.Model.Entities.Products
{
    public abstract class ProductAttributeValue: IEntity
    {
        public Guid Id  { get; set; }

        public List<ProductAttributeValueConnection> ProductConnctions { get; set; }

        public ProductAttribute Attribute { get; set; }

        public Guid AttributeId { get; set; }      
    }
}
