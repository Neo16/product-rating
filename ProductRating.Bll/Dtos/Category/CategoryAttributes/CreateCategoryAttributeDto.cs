using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRating.Bll.Dtos.Category.CategoryAttributes
{
    /// <summary>
    /// Used for:
    /// - Create Category with attributes 
    /// </summary>
    public class CreateCategoryAttributeDto
    {      
        public string AttributeName { get; set; }

        public bool HasFixedValues { get; set; }

        public AttributeType Type { get; set; }

        public List<CreateCategoryAttributeValueDto> Values { get; set; }
    }
}
