using System;

namespace ProductRating.Bll.Dtos.Product
{
    public class ProductHeaderDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string BrandName { get; set; }

        public string CategoryName { get; set; }

        public string ThumbnailImage { get; set; }

        public long? Price { get; set; }

        public double Score { get; set; }
    }
}
