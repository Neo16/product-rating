using ProductRating.Bll.Dtos.Category;
using ProductRating.Bll.Dtos.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRating.Bll.Dtos
{
    public class SearchResultDto
    {
        public List<ProductHeaderDto> Products { get; set; }

        public List<CategoryHeaderDto> Categories { get; set; }

        public List<BrandHeaderDto> Brands { get; set; }

        public long MaxPriceOption { get; set; }
    }
}
