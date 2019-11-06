using System;

namespace ProductRating.Bll.Dtos.Category
{
    public class CategoryHeaderDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int NumOfProducts { get; set; }
    }
}
