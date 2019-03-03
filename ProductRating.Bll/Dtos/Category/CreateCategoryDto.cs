﻿using ProductRating.Bll.Dtos.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRating.Bll.Dtos.Category
{
    public class CreateCategoryDto
    {
        public string Name { get; set; }

        public Guid ParenCategoryId { get; set; }

        public ICollection<AttributeBase> Attributes { get; set; }
    }
}