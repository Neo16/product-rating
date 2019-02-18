using Microsoft.EntityFrameworkCore.Internal;
using ProductRating.Dal;
using ProductRating.Model.Entities.Product;
using ProductRating.Model.Entities.Product.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ProductRating.Bll.Services
{
    public class ProductService
    {
        private readonly ApplicationDbContext context;

        public ProductService(ApplicationDbContext context)
        {
            this.context = context;
        }
 
        public void Test()
        {
           List<Expression<Func<ProductAttributeValue, bool>>> filters = new List<Expression<Func<ProductAttributeValue, bool>>>();

            filters.Add(e => e.Type == "string" && (e as ProductAttributeStringValue).StringValue == "asd");
            FilterForStringAttributes(filters);
        }

        //Todo tesztelni 1 
        IQueryable<Product> FilterForStringAttributes(List<Expression<Func<ProductAttributeValue, bool>>> filters)
        {
            var query = context.Products
                .AsQueryable();

            foreach (var filter in filters)
            {
                query = query.Where(e => e.PropertyValues.AsQueryable().Any(filter));
            }    
            return query;
        }

        //Todo tesztelni 2
        ////Generikusan...
        //IQueryable<Product> FilterForGenericAttributes(List<Expression<Func<ProductAttributeValue<string>, bool>>> filters)
        //{
        //    var query = context.Products
        //        .AsQueryable();

        //    foreach (var filter in filters)
        //    {
        //        query = query.Where(e => e.PropertyValues.OfType<ProductAttributeValue<string>>().AsQueryable().Any(filter));
        //    }
        //    return query;
        //}
    }
}
