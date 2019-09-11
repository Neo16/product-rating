using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ProductRating.Bll.Dtos;
using ProductRating.Bll.Dtos.Category;
using ProductRating.Bll.Dtos.Category.CategoryAttributes;
using ProductRating.Bll.Exceptions;
using ProductRating.Bll.ServiceInterfaces;
using ProductRating.Dal;
using ProductRating.Model.Entities.Products;
using ProductRating.Model.Entities.Products.Attributes;
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

        public async Task<List<CategoryAttributeDto>> GetAttributesOf(Guid categoryId)
        {
            var attributes = await context.ProductAttributes
                .Where(e => e.CategoryId == categoryId)
                .ProjectTo<CategoryAttributeDto>(mapperConfiguration)
                .ToListAsync();

            foreach (var attr in attributes)
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

        public async Task<Guid> CreateCategory(CreateEditCategoryDto category)
        {
            var dbCategory = MapToDbCategory(category);

            bool isCategoryNameTaken = await context.Categories
                .AnyAsync(e => e.Name.ToUpper() == dbCategory.Name.ToUpper());

            if (isCategoryNameTaken)
            {
                throw new BusinessLogicException("A category with the same name already exists.")
                {
                    ErrorCode = ErrorCode.InvalidArgument
                };
            }

            context.Categories.Add(dbCategory);
            await context.SaveChangesAsync();

            return dbCategory.Id;
        }

        public async Task UpdateCategory(Guid categoryId, CreateEditCategoryDto category)
        {
            var oldDbCategory = await context.Categories
                .Include(e => e.Attributes)
                .ThenInclude(e => e.Values)
                .SingleOrDefaultAsync(e => e.Id == categoryId);

            if (oldDbCategory == null)
            {
                throw new BusinessLogicException("The category does not exist.")
                {
                    ErrorCode = ErrorCode.InvalidArgument
                };
            }
            var updatedDbCategory = MapToDbCategory(category);

            //1. Kategória alap adatai frissíteni 
            oldDbCategory.Name = updatedDbCategory.Name;
            oldDbCategory.ParentId = updatedDbCategory.ParentId;
            oldDbCategory.ThumbnailPictureId = updatedDbCategory.ThumbnailPictureId;

            //2. Új attribútumok hozzáadása 
            var newAttributes = updatedDbCategory.Attributes
                .Where(e => !oldDbCategory.Attributes.Select(f => f.Id).Any(g => g == e.Id));

            if (oldDbCategory.Attributes == null)
            {
                oldDbCategory.Attributes = new List<ProductAttribute>();
            }
            foreach (var newAttr in newAttributes)
            {
                oldDbCategory.Attributes.Add(newAttr);
            }

            //3. Régi attribútumok frissítése.
            foreach(var attr in updatedDbCategory.Attributes)
            {
                var oldAttr = oldDbCategory.Attributes
                    .Where(e => e.Id == attr.Id)
                    .SingleOrDefault();

                if (oldAttr != null)
                {
                    if (attr.HasFixedValues)
                    {
                        //3.1 Attribútum név frisítése
                        oldAttr.Name = attr.Name;
                        UpdateAttributeValues(oldAttr, attr);
                    }                    
                }
            }

            //4. Törölt attribútumok törlése, hozzá tartozó value-kkal együtt. 
            var attributesToDelete = oldDbCategory.Attributes
              .Where(e => !updatedDbCategory.Attributes.Select(f => f.Id).Any(g => g == e.Id));
            context.ProductAttributes.RemoveRange(attributesToDelete);


            await context.SaveChangesAsync();
        }

        private void UpdateAttributeValues(ProductAttribute oldAttr, ProductAttribute newAttr)
        {
            //3.2 Attribútum értékek frissítése (itt csak törlés, vagy hozzáadás van)   
            var newValues = newAttr.Values
                .Where(e => !oldAttr.Values.Select(f => f.Id).Contains(e.Id))
                .ToList();
            foreach (var newVal in newValues)
            {
                oldAttr.Values.Add(newVal);
            }
            var removableValues = oldAttr.Values
                .Where(e => !newAttr.Values.Select(f => f.Id).Contains(e.Id))
                .ToList();

            foreach (var val in removableValues)
            {
                oldAttr.Values.Remove(val);
            }
        }

        public async Task DeleteCategory(Guid categoryId)
        {
            bool hasProducts = await context.Categories
                .Where(e => e.Id == categoryId)
                .Where(e => e.Products.Count > 0)
                .AnyAsync();

            if (hasProducts)
            {
                throw new BusinessLogicException(
                    "The category can not be deleted, because there are still products in this category.")
                {
                    ErrorCode = ErrorCode.InvalidArgument
                };
            }

            bool hasSubCategories = await context.Categories
               .AnyAsync(e => e.ParentId == categoryId);

            if (hasSubCategories)
            {
                throw new BusinessLogicException(
                    "The category can not be deleted, because it has subcategories.")
                {
                    ErrorCode = ErrorCode.InvalidArgument
                };
            }

            var category = await context.Categories
              .Include(e => e.Attributes)
              .SingleOrDefaultAsync(e => e.Id == categoryId);

            foreach (var attr in category.Attributes)
            {
                context.Entry(attr).State = EntityState.Deleted;
            }

            context.Categories.Remove(category);
            await context.SaveChangesAsync();
        }

        private Category MapToDbCategory(CreateEditCategoryDto category)
        {
            var dbCategory = mapper.Map<Category>(category);

            dbCategory.Attributes = new List<ProductAttribute>();

            foreach (var catAttrDto in category.Attributes)
            {
                switch (catAttrDto.Type)
                {
                    case AttributeType.String:
                        var cAttrString = mapper.Map<ProductAttributeString>(catAttrDto);
                        if (cAttrString.HasFixedValues || catAttrDto.Values != null)
                        {
                            cAttrString.Values = catAttrDto.Values.Select(e => new ProductAttributeStringValue()
                            {
                                StringValue = e.StringValue,
                                Id = e.ValueId != null ? e.ValueId.Value : new Guid()
                            } as ProductAttributeValue).ToList();
                        }

                        dbCategory.Attributes.Add(cAttrString);
                        break;
                    case AttributeType.Int:
                        var cAttrInt = mapper.Map<ProductAttributeInt>(catAttrDto);
                        if (cAttrInt.HasFixedValues || catAttrDto.Values != null)
                        {
                            cAttrInt.Values = catAttrDto.Values.Select(e => new ProductAttributeIntValue()
                            {
                                IntValue = e.IntValue,
                                Id = e.ValueId != null ? e.ValueId.Value : new Guid()

                            } as ProductAttributeValue).ToList();
                        }

                        dbCategory.Attributes.Add(cAttrInt);
                        break;
                }
            }

            return dbCategory;
        }

        public async Task<CreateEditCategoryDto> GetCategoryForUpdate(Guid categoryId)
        {
            var dbCategory = await context.Categories
                .Include(e => e.Attributes)
                .ThenInclude(e => e.Values)
                .FirstOrDefaultAsync(e => e.Id == categoryId);

            var mappedCategory = mapper.Map<CreateEditCategoryDto>(dbCategory);

            // Attribute values are not editable, only can be added or removed. 
            mappedCategory.Attributes.SelectMany(e => e.Values)
                .Select(e => e.Editable = false);

            return mappedCategory;              
              
        }

        public async Task<List<CategoryManageHeaderDto>> AdminGetCategories(ManageCategoryFilterDto filter, Guid userId, PaginationDto pagination)
        {
            var query = context.Categories
                .Include(e => e.Attributes)
                .Include(e => e.Products) // Todo: select productNum in query 
                .Include(e => e.Parent)
                .AsQueryable();

            //filter
            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                query = query.Where(e => e.Name.ToUpper().Contains(filter.Name.ToUpper()));
            }
            if (filter.IsMine == true)
            {
                query = query.Where(e => e.CreatorId == userId);
            }

            //pagination            
            if (pagination.Start != null && pagination.Length != null)
            {
                query = query.Skip(pagination.Start.Value - 1).Take(pagination.Length.Value);
            }

            // ProjectTo doesnt work for this mapping (has string.join)
            var categories = await query.ToListAsync();

            return categories
                .Select(e => mapper.Map<CategoryManageHeaderDto>(e))
                .ToList();              
        }
    }
}
