using ProductRating.Bll.Dtos.Category;
using ProductRating.Bll.Dtos.Product;
using System.Collections.Generic;

namespace ProductRating.Bll.Dtos
{
    public class SearchResultDto
    {
        public List<ProductHeaderDto> Products { get; set; }

        public List<CategoryHeaderDto> Categories { get; set; }

        public List<BrandHeaderDto> Brands { get; set; }      

        public int TotalNumOfResults  { get; set; }
    }
}
