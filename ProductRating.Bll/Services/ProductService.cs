using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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
 
        public async Task Test()
        {
           List<Expression<Func<ProductAttributeValue, bool>>> filters = new List<Expression<Func<ProductAttributeValue, bool>>>();

            filters.Add(e => e.Type == "ProductAttributeStringValue" && (e as ProductAttributeStringValue).StringValue == "Ez az érték");
            var query = FilterForStringAttributes(filters);

            var result = query.ToList();

            return;
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
