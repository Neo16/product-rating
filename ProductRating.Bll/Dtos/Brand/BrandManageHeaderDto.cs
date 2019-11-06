using System;

namespace ProductRating.Bll.Dtos.Brand
{
    public class BrandManageHeaderDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int NumOfProducts { get; set; }

        //List of categories of products of this brand 
        // as a comma separated string 
        public string Categories { get; set; }

        public Guid CreatorId { get; set; }

        public bool? IsCreatedByMe { get; set; }
    }
}
