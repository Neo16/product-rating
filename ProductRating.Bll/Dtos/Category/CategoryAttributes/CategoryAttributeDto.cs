using System;
using System.Collections.Generic;

namespace ProductRating.Bll.Dtos.Category.CategoryAttributes
{
    /// <summary>
    /// Used for:
    /// - Telling the frontend what kind of attributes can be given to filter a category   
    /// </summary>
    public class CategoryAttributeDto
    {
        public Guid AttributeId { get; set; }

        public string AttributeName { get; set; } 

        public bool HasFixedValues { get; set; }

        public AttributeType Type { get; set; }

        public List<CategoryAttributeValueDto> Values { get; set; }
    }
}
