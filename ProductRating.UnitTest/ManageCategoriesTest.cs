using Autofac;
using Microsoft.EntityFrameworkCore;
using ProductRating.Bll.Dtos.Category;
using ProductRating.Bll.Dtos.Category.CategoryAttributes;
using ProductRating.Bll.ServiceInterfaces;
using ProductRating.Dal;
using ProductRating.Dal.Model.Entities.Products.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace ProductRating.UnitTest
{
    [UseAutofacTestFramework]
    public class ManageCategoriesTest : DatabaseFixture
    {
        private readonly ICategoryService categoryService;

        private CreateEditCategoryDto categoryToBeInserted = new CreateEditCategoryDto()
        {
            Name = "Toys",
            Attributes = new List<CreateEditCategoryAttributeDto>()
               {
                   new CreateEditCategoryAttributeDto()
                   {
                       AttributeName = "Size",
                       Type = AttributeType.Int,
                       HasFixedValues = false
                   },
                   new CreateEditCategoryAttributeDto()
                   {
                       AttributeName = "Color",
                       Type = AttributeType.String,
                       HasFixedValues = true,
                       Values = new List<CreateEditCategoryAttributeValueDto>()
                       {
                           new CreateEditCategoryAttributeValueDto()
                           {
                               StringValue = "Red"
                           }
                       }
                   }
               }
        };

        public ManageCategoriesTest(
            ApplicationDbContext context,
            ICategoryService categoryService): base(context)
        {            
            this.categoryService = categoryService;
        }

        [Fact]
        public async Task CreateCategory()
        {
            context.Database.EnsureDeleted();
            await categoryService.CreateCategory(categoryToBeInserted);

            var dbCategory = context.Categories
                .Include(e => e.Attributes)
                .ThenInclude(e => e.Values)
                .FirstOrDefault();

            Assert.NotNull(dbCategory);
            Assert.Equal("Toys", dbCategory.Name);
            Assert.Equal(2, dbCategory.Attributes.Count());
            Assert.Contains(dbCategory.Attributes, e => e.Name == "Color");

            var colorAttr = dbCategory.Attributes
                .Where(e => e.Name == "Color")
                .Single();

            Assert.Contains(colorAttr.Values, e => (e as ProductAttributeStringValue).StringValue == "Red");
        }

        [Fact]
        public async Task UpdateCategory()
        {
            context.Database.EnsureDeleted();
            var insertedCategoryId = await categoryService.CreateCategory(categoryToBeInserted);
            var categoryToChangeDto = await categoryService.GetCategoryForUpdate(insertedCategoryId);

            categoryToChangeDto.Name = "Changed";
            await categoryService.UpdateCategory(insertedCategoryId, categoryToChangeDto);

            var updatedDbCategory = context.Categories
                .Single(e => e.Id == insertedCategoryId);

            Assert.NotNull(updatedDbCategory);
            Assert.Equal("Changed", updatedDbCategory.Name);
        }

        [Fact]
        public async Task UpdateCategoryWithAttributeChange()
        {
            context.Database.EnsureDeleted();
            var insertedCategoryId = await categoryService.CreateCategory(categoryToBeInserted);
            var categoryToChangeDto = await categoryService.GetCategoryForUpdate(insertedCategoryId);

            categoryToChangeDto.Attributes.First().AttributeName = "ChangedAttribute";
            await categoryService.UpdateCategory(insertedCategoryId, categoryToChangeDto);

            var updatedDbCategory = context.Categories
               .Single(e => e.Id == insertedCategoryId);

            Assert.Contains(updatedDbCategory.Attributes, e => e.Name == "ChangedAttribute");
        }

        [Fact]
        public async Task UpdateCategoryWithAttributeDelete()
        {
            context.Database.EnsureDeleted();
            var insertedCategoryId = await categoryService.CreateCategory(categoryToBeInserted);
            var categoryToChangeDto = await categoryService.GetCategoryForUpdate(insertedCategoryId);

            categoryToChangeDto.Attributes.Remove(categoryToChangeDto.Attributes.First());
            await categoryService.UpdateCategory(insertedCategoryId, categoryToChangeDto);

            var updatedDbCategory = context.Categories
               .Single(e => e.Id == insertedCategoryId);

            Assert.Single(updatedDbCategory.Attributes);
        }

        [Fact]
        public async Task UpdateCategoryWithAttributeAdd()
        {
            context.Database.EnsureDeleted();
            var insertedCategoryId = await categoryService.CreateCategory(categoryToBeInserted);
            var categoryToChangeDto = await categoryService.GetCategoryForUpdate(insertedCategoryId);

            categoryToChangeDto.Attributes.Add(new CreateEditCategoryAttributeDto()
            {
                AttributeName = "New Attribute",
                HasFixedValues = true,
                Type = AttributeType.String,
                Values = new List<CreateEditCategoryAttributeValueDto>()
                {
                    new CreateEditCategoryAttributeValueDto(){StringValue = "New Value Option"}
                }
            });
            await categoryService.UpdateCategory(insertedCategoryId, categoryToChangeDto);

            var updatedDbCategory = context.Categories
               .Include(e => e.Attributes)
               .ThenInclude(e => e.Values)
               .Single(e => e.Id == insertedCategoryId);

            Assert.Contains(updatedDbCategory.Attributes, e => e.Name == "New Attribute");   
            Assert.Contains(context.ProductAttributeValues.OfType<ProductAttributeStringValue>(),
                e => e.StringValue == "New Value Option");
        }

        [Fact]
        public async Task UpdateCategoryWithAttributeValueAdd()
        {
            context.Database.EnsureDeleted();
            var insertedCategoryId = await categoryService.CreateCategory(categoryToBeInserted);
            var categoryToChangeDto = await categoryService.GetCategoryForUpdate(insertedCategoryId);

            var fixedValueAttr = categoryToChangeDto.Attributes.Where(e => e.HasFixedValues).First();
            fixedValueAttr.Values.Add(new CreateEditCategoryAttributeValueDto() { StringValue = "New Value" });

            await categoryService.UpdateCategory(insertedCategoryId, categoryToChangeDto);

            Assert.Contains(context.ProductAttributeValues.OfType<ProductAttributeStringValue>(),
                 e => e.StringValue == "New Value");
        }

        [Fact]
        public async Task UpdateCategoryWithAttributeValueDelete()
        {
            context.Database.EnsureDeleted();
            var insertedCategoryId = await categoryService.CreateCategory(categoryToBeInserted);
            var categoryToChangeDto = await categoryService.GetCategoryForUpdate(insertedCategoryId);

            var fixedValueAttr = categoryToChangeDto.Attributes.Where(e => e.HasFixedValues).First();
            fixedValueAttr.Values.Remove(fixedValueAttr.Values.First());

            await categoryService.UpdateCategory(insertedCategoryId, categoryToChangeDto);

            var changed = await categoryService.GetCategoryForUpdate(insertedCategoryId);
            var changedFixedValueAttr = changed.Attributes.Where(e => e.HasFixedValues).First();

            Assert.Empty(changedFixedValueAttr.Values);
        }

        [Fact]
        public async Task DeleteCategory()
        {
            context.Database.EnsureDeleted();
            var insertedCategoryId = await categoryService.CreateCategory(categoryToBeInserted);

            await categoryService.DeleteCategory(insertedCategoryId);

            Assert.DoesNotContain(insertedCategoryId, context.Categories.Select(e => e.Id));
        }
    }
}
