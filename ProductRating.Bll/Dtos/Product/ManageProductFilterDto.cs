using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRating.Bll.Dtos.Product
{
    public class ManageProductFilterDto
    {
        public Guid? CategoryId { get; set; }

        public List<Guid> BrandIds { get; set; }

        public string Name { get; set; }

        public bool? IsMine { get; set; }
    }
}
