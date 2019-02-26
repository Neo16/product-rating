using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using ProductRating.Bll.Dtos.Product;
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
            foreach (var stringFilter in filterDto.StringAttributeFilters)
            {
                filters.Add(e => e.Type == "ProductAttributeStringValue" && e.ProductAttribute.Name == stringFilter.AttributeName
                        && (e as ProductAttributeStringValue).StringValue == stringFilter.Value);
            }

            var query = FilterForAttributes(filters);
            var result = await query.ToListAsync();
            return result;
        }

        IQueryable<Product> FilterForAttributes(List<Expression<Func<ProductAttributeValue, bool>>> filters)
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
