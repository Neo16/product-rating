using System;
using System.Collections.Generic;

namespace ProductRating.Bll.Dtos.Product
{
    public class ProductFilterDto
    {
        public Guid? CategoryId { get; set; }

        public Guid? BrandId { get; set; }

        public string TextSearch { get; set; }

        public List<AttributeBase> Attributes { get; set; }

        public ProductOrder OrderBy { get; set; }
    }
}
