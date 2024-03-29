﻿using AutoMapper;
using ProductRating.Bll.Dtos.Brand;
using ProductRating.Dal.Model.Entities.Products;
using System.Linq;

namespace ProductRating.Bll.Mappings
{
    public class BrandMappingProfile : Profile
    {
        public BrandMappingProfile()
        {
            this.CreateMap<Brand, BrandManageHeaderDto>()
              .ForMember(e => e.CreatorId, e => e.MapFrom(f => f.CreatorId))
              .ForMember(e => e.Id, e => e.MapFrom(f => f.Id))
              .ForMember(e => e.Name, e => e.MapFrom(f => f.Name))
              .ForMember(e => e.NumOfProducts, e => e.MapFrom(f => f.Products.Count))
              .ForMember(e => e.Categories,
                    e => e.MapFrom(f => string.Join(", ", f.Products.Select(d => d.Category.Name).Distinct()))
               );
        }
    }
}
