using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRating.Model.Entities.Products
{
    public class ProductAttributeValueConnection
    {      
        public Product Product { get; set; }

        public Guid ProductId { get; set; }

        public ProductAttributeValue ProductAttributeValue { get; set; }

        public Guid ProductAttributeValueId { get; set; }
    }
}
