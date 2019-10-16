using ProductRating.Dal.Model.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRating.Dal.Model.Entities.Products
{
    public class Offer
    {
        public Guid SellerId { get; set; }
        public Guid ProductId { get; set; }
        public ApplicationUser Seller { get; set; }
        public Product Product { get; set; }
        public long Price { get; set; }
        public string Url { get; set; }
    }
}

