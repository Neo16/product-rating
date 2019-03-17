using ProductRating.Bll.Dtos.Product.Attributes;
using System;
using System.Collections.Generic;

namespace ProductRating.Bll.Dtos.Product
{
    public class ProductFilterDto
    {
        public Guid? CategoryId { get; set; }

        public Guid? BrandId { get; set; }

        public string TextSearch { get; set; }

        public List<IntAttribute> IntAttributes { get; set; }

        public List<StringAttribute> StringAttributes { get; set; }

        public ProductOrder? OrderBy { get; set; }
    }
}
