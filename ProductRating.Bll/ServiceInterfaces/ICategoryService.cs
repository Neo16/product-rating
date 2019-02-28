using ProductRating.Bll.Dtos.Category;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductRating.Bll.ServiceInterfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryHeaderDto>> GetMainCategories();

        Task<List<CategoryHeaderDto>> GetChildCategoriesOf(Guid categoryId);

        Task CreateCategory(CreateCategoryDto category);
    }
}
