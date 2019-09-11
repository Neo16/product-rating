using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRating.Bll.Dtos.Category
{
    public class CategoryManageHeaderDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int NumOfProducts { get; set; }
        public string AttributeNames { get; set; }

        public string ParentName { get; set; }
    }
}
