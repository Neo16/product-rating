using System;

namespace ProductRating.Bll.Dtos.Product
{
    public class ProductManageHeaderDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string BrandName { get; set; }

        public string CategoryName { get; set; }

        public string CreatedAt { get; set; }

        public bool? IsCreatedByMe { get; set; }

        public bool? IsSoldByMe { get; set; }


    }
}
