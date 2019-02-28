using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using ProductRating.Bll.Dtos.Product;
using ProductRating.Bll.Dtos.Product.Attributes;
using ProductRating.Bll.ServiceInterfaces;
using ProductRating.Dal;
using ProductRating.Model.Entities.Product;
using ProductRating.Model.Entities.Product.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProductRating.Bll.Services
{
    public class ProductService : ServiceBase, IProductService
    {
        public ProductService(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<List<Product>> Test(ProductFilter filterDto)
        {
            List<Expression<Func<ProductAttributeValue, bool>>> filters = new List<Expression<Func<ProductAttributeValue, bool>>>();

            var stringFilterAttributes = filterDto.Attributes.OfType<StringAttribute>();
            foreach (var stringFilterAttr in stringFilterAttributes)
            {
                filters.Add(e => e.Type == "ProductAttributeStringValue" && e.ProductAttribute.Name == stringFilterAttr.AttributeName
                        && (e as ProductAttributeStringValue).StringValue == stringFilterAttr.Value);
            }

            //Todo int 

            var query = FilterForAttributes(filters);
            var result = await query.ToListAsync();
            return result;
        }

        private IQueryable<Product> FilterForAttributes(List<Expression<Func<ProductAttributeValue, bool>>> filters)
        {
            var query = context.Products
                .AsQueryable();

            foreach (var filter in filters)
            {
                query = query.Where(e => e.PropertyValues.AsQueryable().Any(filter));
            }
            return query;
        }
    }
}
