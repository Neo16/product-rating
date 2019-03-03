using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRating.Bll.Dtos.Product
{
    public class ProductDetailsDto
    {
        public string Name { get; set; }

        public string BrandName { get; set; }

        public Guid BrandId { get; set; }

        public string CategoryName { get; set; }

        public Guid CategoryId { get; set; }

        public ICollection<AttributeBase> Attributes { get; set; }
    }
}
