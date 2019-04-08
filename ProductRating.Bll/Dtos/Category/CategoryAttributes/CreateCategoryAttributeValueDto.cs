using System;

namespace ProductRating.Bll.Dtos.Category.CategoryAttributes
{
    public class CreateEditCategoryAttributeValueDto
    {
        public string StringValue { get; set; }

        public int IntValue { get; set; }

        public Guid? ValueId { get; set; }
    }
}
