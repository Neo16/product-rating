using Autofac;
using Microsoft.EntityFrameworkCore;
using ProductRating.Bll.Dtos.Product;
using ProductRating.Bll.Dtos.Product.Attributes;
using ProductRating.Bll.ServiceInterfaces;
using ProductRating.Dal;
using ProductRating.Model.Entities.Products;
using ProductRating.Model.Entities.Products.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Frameworks.Autofac;

namespace ProductRating.UnitTest
{
    [UseAutofacTestFramework]
    public class ManageProductsTest : DatabaseFixture
    {
        private readonly IProductService productService;    
 

        public ManageProductsTest(
            ApplicationDbContext context,
            IProductService productService ) :base(context)
        {        
            this.productService = productService;          
        }

        private CreateEditProductDto MakeDtoForInsert()
        {
            var category = new Category()
            {
                Name = "Books",
                Attributes = new List<ProductAttribute>()
                {
                    new ProductAttributeString() {Name = "Author" },
                    new ProductAttributeInt() { Name ="Publication date" }
                }
            };

            context.Categories.Add(category);
            context.SaveChanges();


            return new CreateEditProductDto()
            {
                CategoryId = category.Id,
                EndOfProduction = DateTime.Now,
                StartOfProduction = DateTime.Now.AddYears(-2),
                Name = "ExampleProduct",
                StringAttributes = new List<StringAttribute>() {
                    new StringAttribute() {
                        Value = "ASD",
                        AttributeId = category.Attributes.Where(e => e.Name == "Author").Single().Id
                    }},
                IntAttributes = new List<IntAttribute>() {
                    new IntAttribute() {
                        Value = 55,
                        AttributeId = category.Attributes.Where(e => e.Name == "Publication date").Single().Id
                    }},
            };
        }

        [Fact]
        public async Task CreateProduct()
        {
            context.Database.EnsureDeleted();
            await productService.CreateProduct(MakeDtoForInsert());

            var dbProduct = context.Products
                .Include(e => e.Category)
                .Include(e => e.Brand)
                .Include(e => e.PropertyValueConnections)
                .ThenInclude(e => e.ProductAttributeValue.Attribute)
                .FirstOrDefault();

            Assert.NotNull(dbProduct);
            Assert.Equal("ExampleProduct", dbProduct.Name);
            Assert.Contains("ASD",
                dbProduct.PropertyValueConnections
                    .Select(e => e.ProductAttributeValue)
                    .OfType<ProductAttributeStringValue>()
                    .Select(e => e.StringValue));

            Assert.Contains("Author",
             dbProduct.PropertyValueConnections
                 .Select(e => e.ProductAttributeValue)
                 .OfType<ProductAttributeStringValue>()
                 .Select(e => e.Attribute.Name));

            //Todo more checks 
        }

        [Fact]
        public async Task UpdateProduct()
        {
            context.Database.EnsureDeleted();
            var insertedProductId = await productService.CreateProduct(MakeDtoForInsert());
            var productToChangeDto = await productService.GetProductForUpdate(insertedProductId);           
        }

        [Fact]
        public async Task DeleteProduct()
        {
            context.Database.EnsureDeleted();
            var insertedProductId = await productService.CreateProduct(MakeDtoForInsert());

            await productService.DeleteProduct(insertedProductId);
            Assert.DoesNotContain(insertedProductId, context.Products.Select(e => e.Id));
        }
    }
}
