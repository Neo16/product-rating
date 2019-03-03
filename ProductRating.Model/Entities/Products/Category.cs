using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRating.Model.Entities.Products
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
    }
}
