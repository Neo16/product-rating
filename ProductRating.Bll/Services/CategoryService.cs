using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ProductRating.Bll.Dtos.Category;
using ProductRating.Bll.Dtos.Category.CategoryAttributes;
using ProductRating.Bll.Dtos.Product;
using ProductRating.Bll.ServiceInterfaces;
using ProductRating.Dal;
using ProductRating.Model.Entities.Products;
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

        public async Task CreateCategory(CreateCategoryDto category)
        {
            var dbCategory = mapper.Map<Category>(category);

            context.Categories.Add(dbCategory);
            await context.SaveChangesAsync();
        }

        public async Task<List<CategoryAttributeDto>> GetAttributesOf(Guid categoryId)
        {
            var attributes = await context.ProductAttributes              
                .Where(e => e.CategoryId == categoryId)
                .ProjectTo<CategoryAttributeDto>(mapperConfiguration)
                .ToListAsync();

            foreach(var attr in attributes)
            {
                if (attr.HasFixedValues)
                {
                    var values = await context.ProductAttributeValues
                        .Where(e => e.AttributeId == attr.AttributeId)
                        .ProjectTo<CategoryAttributeValueDto>(mapperConfiguration)
                        .ToListAsync();
                    attr.Values = values;
                }
            }
            
            return attributes;
        }
    }
}
