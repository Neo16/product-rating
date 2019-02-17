using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRating.Bll.Dtos.Product
{
    public class ProductData
    {
        public ICollection<AttributeBase> Attributes { get; set; }
    }
}
