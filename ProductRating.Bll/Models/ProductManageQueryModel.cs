using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRating.Bll.Models
{
    public class ProductManageQueryModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string BrandName { get; set; }

        public string CategoryName { get; set; }

        public string CreatedAt { get; set; }

        public Guid CreatorId { get; set; }

        public List<Guid> SellerIds { get; set; }
    }
}
