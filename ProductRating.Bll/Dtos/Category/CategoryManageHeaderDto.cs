﻿using System;

namespace ProductRating.Bll.Dtos.Category
{
    public class CategoryManageHeaderDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int NumOfProducts { get; set; }
        public string AttributeNames { get; set; }

        public string ParentName { get; set; }

        public Guid CreatorId { get; set; }

        public bool? IsCreatedByMe { get; set; }
    }
}
