using AutoMapper;
using ProductRating.Bll.Dtos.Category;
using ProductRating.Model.Entities.Products;

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
        }
    }
}
