using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRating.Bll.Dtos.Category.CategoryAttributes
{
    /// <summary>
    /// Used for:
    /// - Create Category with attributes 
    /// </summary>
    public class CreateEditCategoryAttributeDto
    {      
        public string AttributeName { get; set; }

        public bool HasFixedValues { get; set; }

        public AttributeType Type { get; set; }

        public Guid? AttributeId { get; set; }

        public List<CreateEditCategoryAttributeValueDto> Values { get; set; }
    }
}
