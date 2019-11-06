using System;

namespace ProductRating.Bll.Dtos.Product
{
    /// <summary>
    /// Used for:
    ///  - displaying product attribute value
    ///  - filtering by product attribute value or Id 
    /// </summary>
    public class AttributeBase
    {
        public Guid AttributeId { get; set; }

        public string AttributeName { get; set; }

        public Guid? ValueId { get; set; }
    }
}
