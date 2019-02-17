using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRating.Model.Entities.Product
{
    public class Brand : IEntity
    {
        public string Name { get; set; }
        public Guid Id { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
