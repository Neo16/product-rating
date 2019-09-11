using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
