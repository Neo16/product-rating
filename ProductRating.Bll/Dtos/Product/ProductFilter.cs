using ProductRating.Bll.Dtos.Product.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRating.Bll.Dtos.Product
{
    public class ProductFilter
    {
        public List<StringAttribute> StringAttributeFilters { set; get; }       
    }
}
