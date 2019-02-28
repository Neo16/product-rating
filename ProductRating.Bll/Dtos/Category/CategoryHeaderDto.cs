using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRating.Bll.Dtos.Category
{
    public class CategoryHeaderDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }        
        public List<CategoryHeaderDto> ChildCategories { get; set; }
        public int NumOfProducts { get; set; }
    }
}
