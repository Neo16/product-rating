using ProductRating.Bll.Dtos.Category.CategoryAttributes;
using System;
using System.Collections.Generic;

namespace ProductRating.Bll.Dtos.Category
{
    public class CreateEditCategoryDto
    {
        public string Name { get; set; }

        public Guid? ParentCategoryId { get; set; }

        public ICollection<CreateEditCategoryAttributeDto> Attributes { get; set; }

        public Guid? ThumbnailPictureId { get; set; }
    }
}
