using AutoMapper;
using ProductRating.Bll.Dtos.Category;
using ProductRating.Bll.Dtos.Product;
using ProductRating.Bll.Dtos.Product.Attributes;
using ProductRating.Model.Entities.Products;
using ProductRating.Model.Entities.Products.Attributes;

namespace ProductRating.Bll.Mappings
{
    public class CategoriesMappingProfile : Profile
    {
        public CategoriesMappingProfile()
        {
            this.CreateMap<Category, CategoryHeaderDto>()
                .ForMember(e => e.Id, e => e.MapFrom(f => f.Id))
                .ForMember(e => e.Name, e => e.MapFrom(f => f.Name))
                .ForMember(e => e.NumOfProducts, e => e.MapFrom(f => f.Products.Count))
                .ForMember(e => e.ChildCategories, e => e.MapFrom(f => f.Children));

            this.CreateMap<CreateCategoryDto, Category>()
                .ForMember(e => e.Name, e => e.MapFrom(f => f.Name))
                .ForMember(e => e.ParentId, e => e.MapFrom(f => f.ParentCategoryId))
                .ForMember(e => e.ThumbnailPictureId, e => e.MapFrom(f => f.ThumbnailPictureId))
                .ForMember(e => e.Attributes, e => e.MapFrom(f => f.Attributes));

            this.CreateMap<AttributeBase, ProductAttribute>()
                .ForMember(e => e.Name, e => e.MapFrom(f => f.AttributeName));

            this.CreateMap<StringAttribute, ProductAttributeString>()
                .IncludeBase<AttributeBase, ProductAttribute>();

            this.CreateMap<IntAttribute, ProductAttributeInt>()
                .IncludeBase<AttributeBase, ProductAttribute>();            
        }
    }
}
