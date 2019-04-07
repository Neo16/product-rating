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
            var attributeFilters = MapAttributeFilters(filter);

            var query = context.Products               
             .AsQueryable();
          
            //filter
            query = FilterForAttributes(attributeFilters, query);

            if (filter.BrandId != null)
            {
                query = query.Where(e => e.BrandId == filter.BrandId.Value);
            }
            if (filter.CategoryId != null)
            {
                query = query.Where(e => e.CategoryId == filter.CategoryId.Value);
            }
            
            //ordering 
            if (filter.OrderBy != null)
            {
                switch (filter.OrderBy.Value)
                {
                    case ProductOrder.BestScore:
                        query = query.OrderByDescending(e => e.ScoreValue);
                        break;
                    case ProductOrder.MostScore:
                        query = query.OrderByDescending(e => e.Scores.Count);
                        break;
                    case ProductOrder.MostTextReview:
                        query = query.OrderByDescending(e => e.Reviews.Count);
                        break;
                    case ProductOrder.Newest:
                        query = query.OrderByDescending(e => e.CreatedAt);
                        break;
                }
            }

            var result = await query
                .ProjectTo<ProductHeaderDto>(mapperConfiguration)
                .ToListAsync();
            return result;
        }

        public async Task<ProductDetailsDto> GetDetails(Guid productId)
        {
            var product =  await context.Products
                .Include(e => e.Category)
                .Include(e => e.Brand)
                .Include(e => e.PropertyValues)
                .ThenInclude(e => e.Attribute)
                .Where(e => e.Id == productId)             
                .FirstOrDefaultAsync();

            return mapper.Map<ProductDetailsDto>(product);
        }

        private List<Expression<Func<ProductAttributeValue, bool>>> MapAttributeFilters(ProductFilterDto filter)
        {
            List<Expression<Func<ProductAttributeValue, bool>>> filters = new List<Expression<Func<ProductAttributeValue, bool>>>();

            if (filter.StringAttributes != null)
            {
                foreach (StringAttribute stringFilterAttr in filter.StringAttributes)
                {
                    filters.Add(e => e is ProductAttributeStringValue && e.Attribute.Id == stringFilterAttr.AttributeId
                            && ((e as ProductAttributeStringValue).StringValue == stringFilterAttr.Value
                            || e.AttributeId == stringFilterAttr.ValueId));
                }
            }
            if (filter.IntAttributes != null)
            {
                foreach (IntAttribute intFilterAttr in filter.IntAttributes)
                {
                    filters.Add(e => e is ProductAttributeIntValue && e.Attribute.Id == intFilterAttr.AttributeId
                            && ((e as ProductAttributeIntValue).IntValue == intFilterAttr.Value      
                            || e.AttributeId == intFilterAttr.ValueId));
                }
            }          

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
