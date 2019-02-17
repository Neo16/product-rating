using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRating.Model.Entities.Product
{
    public class ProductProperty : IEntity
    {
        public Guid Id { get; set; }

        public string Name  { get; set; }
    }
}
