using System;
using System.Collections.Generic;

namespace ProductRating.Bll.Dtos.Product
{
    public class ManageProductFilterDto
    {
        public Guid? CategoryId { get; set; }

        public List<Guid> BrandIds { get; set; }

        public string Name { get; set; }

        public bool? IsCreatedByMe { get; set; }

        public bool? IsSoldByMe { get; set; }
    }
}
