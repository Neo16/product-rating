using System.Collections.Generic;

namespace ProductRating.Bll.Dtos.Product
{
    public class ProductFilter
    {
        public ICollection<AttributeBase> Attributes { get; set; }
    }
}
