﻿using ProductRating.Bll.Dtos;
using ProductRating.Bll.Dtos.Category;
using ProductRating.Bll.Dtos.Category.CategoryAttributes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductRating.Bll.ServiceInterfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryManageHeaderDto>> AdminGetCategories(ManageCategoryFilterDto filter, Guid userId, PaginationDto pagination);

        Task<List<CategoryHeaderDto>> GetMainCategories();

        Task<List<CategoryHeaderDto>> GetChildCategoriesOf(Guid categoryId);

        Task<List<CategoryAttributeDto>> GetAttributesOf(Guid categoryId);

        Task<CreateEditCategoryDto> GetCategoryForUpdate(Guid categoryId);

        Task<Guid> CreateCategory(CreateEditCategoryDto category);

        Task UpdateCategory(Guid categoryId, CreateEditCategoryDto category);

        Task DeleteCategory(Guid categoryId);
    }
}
