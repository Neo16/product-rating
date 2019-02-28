using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRating.Model.Entities.Product
{
    public class Product : IEntity
    {       
        public Guid Id { get; set; }

        public string Name { get; set; }
   
        public Brand Brand { get; set; }

        public Guid BrandId { get; set; }

        public DateTime StartOfProduction { get; set; }
        public DateTime EndOfProduction { get; set; }
        public DateTime CreatedAt { get; set; }

        public Category Category { get; set; }

        public Guid CategoryId { get; set; }

        public ICollection<ProductAttributeValue> PropertyValues { get; set; }

        //Todo add pictures 
    }
}
