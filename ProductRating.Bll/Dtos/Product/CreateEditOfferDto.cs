using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRating.Bll.Dtos.Product
{
    public class CreateEditOfferDto
    {
        public long Price { get; set; }
        public string Url { get; set; }
    }
}
