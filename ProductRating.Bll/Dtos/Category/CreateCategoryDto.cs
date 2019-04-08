using ProductRating.Bll.Dtos.Category.CategoryAttributes;
using System;
using System.Collections.Generic;

namespace ProductRating.Bll.Dtos.Category
{
    public class CreateCategoryDto
    {
        public string Name { get; set; }

        public Guid? ParentCategoryId { get; set; }

        public ICollection<CreateCategoryAttributeDto> Attributes { get; set; }

        public Guid? ThumbnailPictureId { get; set; }
    }
}
