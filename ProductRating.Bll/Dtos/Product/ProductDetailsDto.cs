﻿using System;
using System.Collections.Generic;

namespace ProductRating.Bll.Dtos.Product
{
    public class ProductDetailsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string BrandName { get; set; }

        public Guid BrandId { get; set; }

        public string CategoryName { get; set; }

        public Guid CategoryId { get; set; }

        public ICollection<AttributeBase> Attributes { get; set; }

        public List<string> Pictures { get; set; }

        public string ThumbnailImage { get; set; }

        public DateTime StartOfProduction { get; set; }
        public DateTime EndOfProduction { get; set; }

        public double ScoreValue { get; set; }

        public double? ScoreByMe { get; set; }
    }
}
