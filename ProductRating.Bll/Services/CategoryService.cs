using ProductRating.Bll.Dtos.Category;
using ProductRating.Bll.ServiceInterfaces;
using ProductRating.Dal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductRating.Bll.Services
{
    public class CategoryService : ServiceBase, ICategoryService
    {
        public CategoryService(ApplicationDbContext context) : base(context)
        {
        }

        public Task<List<CategoryHeaderDto>> GetMainCategories()
        {
            throw new NotImplementedException();
        }
        public Task<List<CategoryHeaderDto>> GetChildCategoriesOf(Guid categoryId)
        {
            throw new NotImplementedException();
        }

        public Task CreateCategory(CreateCategoryDto category)
        {
            throw new NotImplementedException();
        }
    }
}
