using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRating.Bll.Dtos.Product
{
    public class OfferHeaderDto
    {
        public Guid ShopId { get; set; }
        public string ShopName { get; set; }
        public long Price { get; set; }
        public string Url { get; set; }
        public PictureDto WebShopPicture { get; set; }
    }
}
