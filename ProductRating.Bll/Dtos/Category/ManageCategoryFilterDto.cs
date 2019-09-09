using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRating.Bll.Dtos.Category
{
    public class ManageCategoryFilterDto
    {
        public string Name { get; set; }
        public bool? IsMine { get; set; }
    }
}
