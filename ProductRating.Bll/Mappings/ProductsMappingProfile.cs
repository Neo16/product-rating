using AutoMapper;
using ProductRating.Bll.Dtos.Product;
using ProductRating.Bll.Dtos.Product.Attributes;
using ProductRating.Model.Entities.Products;
using ProductRating.Model.Entities.Products.Attributes;
using System.Linq;

namespace ProductRating.Bll.Mappings
{
    public class ProductsMappingProfile : Profile
    {
        public ProductsMappingProfile()
        {
            this.CreateMap<Product, CreateEditProductDto>()
               .ForMember(e => e.Id, e => e.MapFrom(f => f.Id))
               .ForMember(e => e.Name, e => e.MapFrom(f => f.Name))
               .ForMember(e => e.BrandId, e => e.MapFrom(f => f.BrandId))
               .ForMember(e => e.CategoryId, e => e.MapFrom(f => f.CategoryId))
               .ForMember(e => e.StartOfProduction, e => e.MapFrom(f => f.StartOfProduction))
               .ForMember(e => e.EndOfProduction, e => e.MapFrom(f => f.EndOfProduction))
               .ForMember(e => e.PictureIds, e => e.MapFrom(f => f.Pictures.Select(g => g.PictureId)))
               .ForMember(e => e.ThumbnailPictureId, e => e.MapFrom(f => f.ThumbnailPictureId))
               .ForMember(e => e.ThumbnailPictureString, e => e.Ignore())
               .ForMember(e => e.IntAttributes,
                    e => e.MapFrom(f => f.PropertyValueConnections
                        .Select(g => g.ProductAttributeValue)
                        .OfType<ProductAttributeIntValue>()))
                .ForMember(e => e.StringAttributes,
                    e => e.MapFrom(f => f.PropertyValueConnections
                        .Select(g => g.ProductAttributeValue)
                        .OfType<ProductAttributeStringValue>()))
               .ReverseMap()
               .ForMember(e => e.PropertyValueConnections, e => e.Ignore())
               .ForMember(e => e.ScoreValue, e => e.Ignore())
               .ForMember(e => e.Reviews, e => e.Ignore())
               .ForMember(e => e.Scores, e => e.Ignore());   


            this.CreateMap<Product, ProductDetailsDto>()
                .ForMember(e => e.Id, e => e.MapFrom(f => f.Id))
                .ForMember(e => e.Name, e => e.MapFrom(f => f.Name))
                .ForMember(e => e.Attributes, e => e.MapFrom(f => f.PropertyValueConnections.Select(g => g.ProductAttributeValue)))
                .ForMember(e => e.BrandName, e => e.MapFrom(f => f.Brand.Name))
                .ForMember(e => e.CategoryName, e => e.MapFrom(f => f.Category.Name));
            
             this.CreateMap<ProductAttributeValue, AttributeBase>()
                 .ForMember(e => e.AttributeName, e => e.MapFrom(f => f.Attribute.Name))
                 .ForMember(e => e.AttributeId, e => e.MapFrom(f => f.Attribute.Id))
                 .ForMember(e => e.ValueId, e => e.MapFrom(f => f.Id));

            this.CreateMap<ProductAttributeIntValue, IntAttribute>()
               .IncludeBase<ProductAttributeValue, AttributeBase>()
               .ForMember(e => e.Value, e => e.MapFrom(f => f.IntValue));

            this.CreateMap<ProductAttributeStringValue, StringAttribute>()
                .IncludeBase<ProductAttributeValue, AttributeBase>()
                .ForMember(e => e.Value, e => e.MapFrom(f => f.StringValue));

        }
    }
}
