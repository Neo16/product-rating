using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ProductRating.Bll.Dtos.Category;
using ProductRating.Bll.ServiceInterfaces;
using ProductRating.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductRating.Bll.Services
{
    public class CategoryService : ServiceBase, ICategoryService
    {
        private readonly IMapper mapper;
        private readonly MapperConfiguration mapperConfiguration;

        public CategoryService(
            ApplicationDbContext context,
            IMapper mapper,
            MapperConfiguration mapperConfiguration
            ) : base(context)
        {
            this.mapperConfiguration = mapperConfiguration;
            this.mapper = mapper;
        }

        public async Task<List<CategoryHeaderDto>> GetMainCategories()
        {
            return await context.Categories
                .Include(e => e.Children)
                .ProjectTo<CategoryHeaderDto>(mapperConfiguration)
                .ToListAsync();                           
        }
        public async Task<List<CategoryHeaderDto>> GetChildCategoriesOf(Guid categoryId)
        {
            return await context.Categories
                .Include(e => e.Children)
                .Where(e => e.ParentId == categoryId)
                .ProjectTo<CategoryHeaderDto>(mapperConfiguration)
                .ToListAsync();
        }

        public Task CreateCategory(CreateCategoryDto category)
        {
            //Todo: 
            throw new NotImplementedException();
        }
    }
}
