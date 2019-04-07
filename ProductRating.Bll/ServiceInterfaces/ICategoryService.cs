using ProductRating.Bll.Dtos.Category;
using ProductRating.Bll.Dtos.Category.CategoryAttributes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductRating.Bll.ServiceInterfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryHeaderDto>> GetMainCategories();

        Task<List<CategoryHeaderDto>> GetChildCategoriesOf(Guid categoryId);

        Task CreateCategory(CreateCategoryDto category);

        Task<List<CategoryAttributeDto>> GetAttributesOf(Guid categoryId);
    }
}
