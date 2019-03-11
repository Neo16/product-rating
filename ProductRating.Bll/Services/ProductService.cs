using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using ProductRating.Bll.Dtos;
using ProductRating.Bll.Dtos.Product;
using ProductRating.Bll.Dtos.Product.Attributes;
using ProductRating.Bll.ServiceInterfaces;
using ProductRating.Dal;
using ProductRating.Model.Entities.Products;
using ProductRating.Model.Entities.Products.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProductRating.Bll.Services
{
    public class ProductService : ServiceBase, IProductService
    {
        private readonly IMapper mapper;
        private readonly MapperConfiguration mapperConfiguration;

        public ProductService(
            ApplicationDbContext context,
            IMapper mapper,
            MapperConfiguration mapperConfiguration) : base(context)
        {
            this.mapperConfiguration = mapperConfiguration;
            this.mapper = mapper;
        }

        public async Task<List<ProductHeaderDto>> Find(ProductFilterDto filter, PaginationDto pagination)
        {
            var attributeFilters = MapAttributeFilters(filter.Attributes);

            var query = context.Products               
             .AsQueryable();
          
            query = FilterForAttributes(attributeFilters, query);

            //Todo filter for: category, brand etc 

            var result = await query
                .ProjectTo<ProductHeaderDto>(mapperConfiguration)
                .ToListAsync();
            return result;
        }

        public Task<ProductDetailsDto> GetDetails(Guid productId)
        {
            throw new NotImplementedException();
        }

        private List<Expression<Func<ProductAttributeValue, bool>>> MapAttributeFilters(List<AttributeBase> filterDtoAttributes)
        {
            List<Expression<Func<ProductAttributeValue, bool>>> filters = new List<Expression<Func<ProductAttributeValue, bool>>>();

            var stringFilterAttributes = filterDtoAttributes.OfType<StringAttribute>();
            foreach (var stringFilterAttr in stringFilterAttributes)
            {
                filters.Add(e => e is ProductAttributeStringValue && e.Attribute.Name == stringFilterAttr.AttributeName
                        && (e as ProductAttributeStringValue).StringValue == stringFilterAttr.Value);
            }

            //Todo int... 

            return filters;
        }

        private static IQueryable<Product> FilterForAttributes(List<Expression<Func<ProductAttributeValue, bool>>> filters, IQueryable<Product> query)
        {
            foreach (var filter in filters)
            {
                query = query.Where(e => e.PropertyValues.AsQueryable().Any(filter));
            }
            return query;
        }
    }
}
