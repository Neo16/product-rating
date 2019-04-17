using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using ProductRating.Bll.Dtos;
using ProductRating.Bll.Dtos.Category;
using ProductRating.Bll.Dtos.Product;
using ProductRating.Bll.Dtos.Product.Attributes;
using ProductRating.Bll.Exceptions;
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

        public async Task<SearchResultDto> Find(ProductFilterDto filter, PaginationDto pagination)
        {
            var attributeFilters = MapAttributeFilters(filter);

            var query = context.Products
                .Include(e => e.Brand)
                .Include(e => e.Category)
                .ThenInclude(e => e.Parent)
                .ThenInclude(e => e.Parent)
                .AsQueryable();

            //filter
            query = FilterForAttributes(attributeFilters, query);

            if (!string.IsNullOrEmpty(filter.TextFilter))
            {
                query = query.Where(e => e.Name.ToUpper().Contains(filter.TextFilter.ToUpper())
                 || e.Category.Name.ToUpper().Contains(filter.TextFilter.ToUpper()));
            }
            if (filter.BrandId != null)
            {
                query = query.Where(e => e.BrandId == filter.BrandId.Value);
            }
            if (filter.CategoryId != null)
            {
                // three lvl deep
                query = query.Where(e =>
                      e.CategoryId == filter.CategoryId.Value
                   || e.Category.Parent.Id == filter.CategoryId.Value
                   || e.Category.Parent.Parent.Id == filter.CategoryId.Value);
            }

            var maxPriceOption = (await query.MaxAsync(e => e.Price) + 100);
            if (maxPriceOption == 0)
            {
                maxPriceOption = 1000;
            }
            if (filter.MinimumPrice!= null && filter.MaximumPrice!= null)
            {
                query = query.Where(e => e.Price >= filter.MinimumPrice && e.Price <= filter.MaximumPrice);
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

            var products = await query                
                .ToListAsync();

            var result = new SearchResultDto() {
                Products = products.Select(e => mapper.Map<ProductHeaderDto>(e)).ToList(),
                Brands = products.Select(e => e.Brand).Distinct().Select(e => mapper.Map<BrandHeaderDto>(e)).ToList(),
                Categories = GetRootCategories(products.Select(e => e.Category)).Select(e => mapper.Map<CategoryHeaderDto>(e)).ToList(),
                MaxPriceOption = maxPriceOption
            };

            return result;
        }

        private List<Category> GetRootCategories(IEnumerable<Category> caegories)
        {
            List<Category> result = new List<Category>();

            foreach (var category in caegories)
            {
                var currentCategory = category;              
                while (currentCategory.Parent != null)
                {                  
                    context.Entry(currentCategory).Reference(e => e.Parent).Load();
                    currentCategory = currentCategory.Parent;                    
                }
                result.Add(currentCategory);
            }
            return result.Distinct().ToList();
        }

        public async Task<ProductDetailsDto> GetDetails(Guid productId)
        {
            var product = await context.Products
                .Include(e => e.Category)
                .Include(e => e.Brand)
                .Include(e => e.PropertyValueConnections.Select(f => f.ProductAttributeValue))
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
                query = query.Where(e => e.PropertyValueConnections.Select(f => f.ProductAttributeValue).AsQueryable().Any(filter));
            }
            return query;
        }

        public async Task<CreateEditProductDto> GetProductForUpdate(Guid productId)
        {
            var dbProduct = await context.Products              
                .Include(e => e.ThumbnailPicture)
                .Include(e => e.PropertyValueConnections)
                .ThenInclude(e => e.ProductAttributeValue)
                .ThenInclude(e => e.Attribute)
                .FirstOrDefaultAsync(e =>e.Id == productId);
          
            return mapper.Map<CreateEditProductDto>(dbProduct);            
        }

        public async Task<Guid> CreateProduct(CreateEditProductDto product)
        {
            //Todo: more validation 

            bool isProductNameTaken = await context.Products
               .AnyAsync(e => e.Name.ToUpper() == product.Name.ToUpper());

            if (isProductNameTaken)
            {
                throw new BusinessLogicException("A product with the same name already exists.")
                {
                    ErrorCode = ErrorCode.InvalidArgument
                };
            }

            var dbProduct = mapper.Map<Product>(product);

            dbProduct.PropertyValueConnections = new List<ProductAttributeValueConnection>();

            foreach(var attr in product.IntAttributes)
            {
                dbProduct.PropertyValueConnections.Add(new ProductAttributeValueConnection()
                {
                    ProductAttributeValue = new ProductAttributeIntValue()
                    {
                        AttributeId = attr.AttributeId,
                        IntValue  = attr.Value,
                        Id = attr.ValueId
                    }                    
                });
            }

            foreach (var attr in product.StringAttributes)
            {
                dbProduct.PropertyValueConnections.Add(new ProductAttributeValueConnection()
                {
                    ProductAttributeValue = new ProductAttributeStringValue()
                    {
                        AttributeId = attr.AttributeId,
                        StringValue = attr.Value,
                        Id = attr.ValueId
                    }
                });
            }           

            context.Products.Add(dbProduct);
            await context.SaveChangesAsync();
            return dbProduct.Id;
        }

        public Task UpdateProduct(Guid productId, CreateEditProductDto product)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteProduct(Guid productId)
        {
            var product = await context.Products
             .Include(e => e.PropertyValueConnections)
             .ThenInclude(e => e.ProductAttributeValue)
             .ThenInclude(e => e.ProductConnctions)
             .Include(e => e.PropertyValueConnections)
             .ThenInclude(e => e.ProductAttributeValue)
             .ThenInclude(e => e.Attribute)
             .SingleOrDefaultAsync(e => e.Id == productId);

            foreach (var attrValue in product.PropertyValueConnections.Select(e => e.ProductAttributeValue))
            {                
                // If the value is not connected to more products 
                // and is not a value of a fixed value attribute, it can be deleted  
                if (attrValue.ProductConnctions.Count == 1 && !attrValue.Attribute.HasFixedValues)
                {
                    context.Entry(attrValue).State = EntityState.Deleted;
                }               
            }
            await context.SaveChangesAsync();

            context.Products.Remove(product);
            await context.SaveChangesAsync();
        }
    }
}
