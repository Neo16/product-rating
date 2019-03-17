using AutoMapper;
using ProductRating.Bll.Dtos.Product;
using ProductRating.Bll.Dtos.Product.Attributes;
using ProductRating.Model.Entities.Products;
using ProductRating.Model.Entities.Products.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductRating.Bll.Mappings
{
    public class ProductsMappingProfile : Profile
    {
        public ProductsMappingProfile()
        {
            this.CreateMap<Product, ProductDetailsDto>()
                .ForMember(e => e.Id, e => e.MapFrom(f => f.Id))
                .ForMember(e => e.Name, e => e.MapFrom(f => f.Name))
                .ForMember(e => e.Attributes, e => e.MapFrom(f => f.PropertyValues))
                .ForMember(e => e.BrandName, e => e.MapFrom(f => f.Brand.Name))
                .ForMember(e => e.CategoryName, e => e.MapFrom(f => f.Category.Name));
            
             this.CreateMap<ProductAttributeValue, AttributeBase>()
                 .ForMember(e => e.AttributeName, e => e.MapFrom(f => f.Attribute.Name));

            this.CreateMap<ProductAttributeIntValue, IntAttribute>()
               .IncludeBase<ProductAttributeValue, AttributeBase>()
               .ForMember(e => e.Value, e => e.MapFrom(f => f.IntValue));

            this.CreateMap<ProductAttributeStringValue, StringAttribute>()
                .IncludeBase<ProductAttributeValue, AttributeBase>()
                .ForMember(e => e.Value, e => e.MapFrom(f => f.StringValue));

        }
    }
}
