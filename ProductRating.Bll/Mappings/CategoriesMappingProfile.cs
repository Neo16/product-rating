using AutoMapper;
using ProductRating.Bll.Dtos.Category;
using ProductRating.Bll.Dtos.Category.CategoryAttributes;
using ProductRating.Bll.Dtos.Product;
using ProductRating.Bll.Dtos.Product.Attributes;
using ProductRating.Model.Entities.Products;
using ProductRating.Model.Entities.Products.Attributes;
using System.Linq;

namespace ProductRating.Bll.Mappings
{
    public class CategoriesMappingProfile : Profile
    {
        public CategoriesMappingProfile()
        {
            this.CreateMap<Category, CategoryHeaderDto>()
                .ForMember(e => e.Id, e => e.MapFrom(f => f.Id))
                .ForMember(e => e.Name, e => e.MapFrom(f => f.Name))
                .ForMember(e => e.NumOfProducts, e => e.MapFrom(f => f.Products.Count));                

            this.CreateMap<CreateCategoryDto, Category>()
                .ForMember(e => e.Name, e => e.MapFrom(f => f.Name))
                .ForMember(e => e.ParentId, e => e.MapFrom(f => f.ParentCategoryId))
                .ForMember(e => e.ThumbnailPictureId, e => e.MapFrom(f => f.ThumbnailPictureId))
                .ForMember(e => e.Attributes, e => e.MapFrom(f => f.Attributes));

            this.CreateMap<AttributeBase, ProductAttribute>()
                .ForMember(e => e.Name, e => e.MapFrom(f => f.AttributeName))
                .ForMember(e => e.Id, e => e.MapFrom(f => f.AttributeId))
                .ReverseMap();


            this.CreateMap<StringAttribute, ProductAttributeString>()
                .IncludeBase<AttributeBase, ProductAttribute>()
                .ReverseMap();

            this.CreateMap<IntAttribute, ProductAttributeInt>()
                .IncludeBase<AttributeBase, ProductAttribute>()
                .ReverseMap();

            this.CreateMap<ProductAttribute, CategoryAttributeDto>()
                 .ForMember(e => e.AttributeName, e => e.MapFrom(f => f.Name))
                 .ForMember(e => e.AttributeId, e => e.MapFrom(f => f.Id))
                 .ForMember(e => e.HasFixedValues, e => e.MapFrom(f => f.HasFixedValues))
                 .ForMember(e => e.Values, e => e.Ignore()) // non fixed values could mean huge datasets 
                 .ForMember(e => e.Type, e => e.MapFrom(f => f is ProductAttributeInt
                        ? AttributeType.Int
                        : AttributeType.String));                

            this.CreateMap<ProductAttributeValue, CategoryAttributeValueDto>()
                 .ForMember(e => e.ValueId, e => e.MapFrom(f => f.Id))
                 .ForMember(e => e.DisplayValue, e => e.MapFrom(f =>
                        f is ProductAttributeIntValue 
                        ? (f as ProductAttributeIntValue).IntValue.ToString() 
                        : (f as ProductAttributeStringValue).StringValue));
        }
    }
}
