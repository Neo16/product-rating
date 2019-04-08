using ProductRating.Bll.Dtos.Product.Attributes;
using System;
using System.Collections.Generic;

namespace ProductRating.Bll.Dtos.Product
{
    public class CreateEditProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } 

        public Guid BrandId { get; set; }   

        public Guid CategoryId { get; set; }

        public List<IntAttribute> IntAttributes { get; set; }

        public List<StringAttribute> StringAttributes { get; set; }

        public List<Guid> PictureIds { get; set; }

        public Guid? ThumbnailPictureId { get; set; }

        public string ThumbnailPictureString { get; set; }

        public DateTime StartOfProduction { get; set; }
        public DateTime EndOfProduction { get; set; }
    }
}
