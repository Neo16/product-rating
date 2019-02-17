using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRating.Model.Entities.Product
{
    public class CategoryProductProperty : IEntity
    {
        public Guid Id { get; set; }

        public Category Category { get; set; }

        public Guid CategoryId { get; set; }

        public ProductProperty ProductPropertyType { get; set; }

        public Guid ProductPropertyTypeId { get; set; }
    }
}
