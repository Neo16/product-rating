using ProductRating.Bll.Dtos.Product.Attributes;
using System;
using System.Collections.Generic;

namespace ProductRating.Bll.Dtos.Product
{
    public class CreateEditProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Guid? BrandId { get; set; }

        public Guid CategoryId { get; set; }

        public List<IntAttribute> IntAttributes { get; set; }

        public List<StringAttribute> StringAttributes { get; set; }

        public List<PictureDto> Pictures { get; set; }

        public PictureDto ThumbnailPicture { get; set; }

        public SimpleDateData StartOfProduction { get; set; }
        public SimpleDateData EndOfProduction { get; set; }
    }
}
