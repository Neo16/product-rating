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

            this.CreateMap<ProductAttribute, CreateEditCategoryAttributeDto>()
                .ForMember(e => e.AttributeName, e => e.MapFrom(f => f.Name))
                .ForMember(e => e.HasFixedValues, e => e.MapFrom(f => f.HasFixedValues))
                .ForMember(e => e.AttributeId, e => e.MapFrom(f => f.Id))
                .ForMember(e => e.Values, e => e.MapFrom(f => f.Values))
                .ForMember(e => e.Type, e => e.MapFrom(f => f is ProductAttributeInt ? AttributeType.Int : AttributeType.String));

            this.CreateMap<ProductAttributeString, CreateEditCategoryAttributeDto>()
               .IncludeBase<ProductAttribute, CreateEditCategoryAttributeDto>();

            this.CreateMap<ProductAttributeInt, CreateEditCategoryAttributeDto>()
               .IncludeBase<ProductAttribute, CreateEditCategoryAttributeDto>();

            this.CreateMap<ProductAttributeValue, CreateEditCategoryAttributeValueDto>()
                 .ForMember(e => e.ValueId, e => e.MapFrom(f => f.Id));

            this.CreateMap<ProductAttributeStringValue, CreateEditCategoryAttributeValueDto>()
                 .IncludeBase<ProductAttributeValue, CreateEditCategoryAttributeValueDto>()
                 .ForMember(e => e.StringValue, e => e.MapFrom(f => f.StringValue));

            this.CreateMap<ProductAttributeIntValue, CreateEditCategoryAttributeValueDto>()
                 .IncludeBase<ProductAttributeValue, CreateEditCategoryAttributeValueDto>()
                 .ForMember(e => e.IntValue, e => e.MapFrom(f => f.IntValue));

            this.CreateMap<Category, CreateEditCategoryDto>()
                .ForMember(e => e.Name, e => e.MapFrom(f => f.Name))
                .ForMember(e => e.ParentCategoryId, e => e.MapFrom(f => f.ParentId))
                .ForMember(e => e.ThumbnailPictureId, e => e.MapFrom(f => f.ThumbnailPictureId))
                .ForMember(e => e.Attributes, e => e.MapFrom(f => f.Attributes))
                .ReverseMap()
                .ForMember(e => e.Attributes, e => e.Ignore());

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

            this.CreateMap<CreateEditCategoryAttributeDto, ProductAttribute>()
                 .ForMember(e => e.Name, e => e.MapFrom(f => f.AttributeName))
                 .ForMember(e => e.HasFixedValues, e => e.MapFrom(f => f.HasFixedValues))
                 .ForMember(e => e.Id, e => e.MapFrom(f => f.AttributeId != null ? f.AttributeId.Value : new System.Guid()))
                 .ForMember(e => e.Values, e => e.Ignore());

            this.CreateMap<CreateEditCategoryAttributeDto, ProductAttributeString>()
                .IncludeBase<CreateEditCategoryAttributeDto, ProductAttribute>();

            this.CreateMap<CreateEditCategoryAttributeDto, ProductAttributeInt>()
               .IncludeBase<CreateEditCategoryAttributeDto, ProductAttribute>();
        }
    }
}
