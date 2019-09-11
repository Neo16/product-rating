using ProductRating.Bll.Dtos.Product.Attributes;
using System;
using System.Collections.Generic;

namespace ProductRating.Bll.Dtos.Product
{
    public class ProductFilterDto
    {
        public Guid? CategoryId { get; set; }

        public List<Guid> BrandIds { get; set; }

        public decimal? MaximumPrice { get; set; }

        public decimal? MinimumPrice { get; set; }

        public string TextFilter { get; set; }

        public List<IntAttribute> IntAttributes { get; set; }

        public List<StringAttribute> StringAttributes { get; set; }

        public ProductOrder? OrderBy { get; set; }

        public Order? Order { get; set; }
    }
}
