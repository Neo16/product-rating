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

        public ProductAttribute ProductAttribute { get; set; }

        public Guid ProductAttributeId { get; set; }  

        public string Type  { get; set; }
    }
}
