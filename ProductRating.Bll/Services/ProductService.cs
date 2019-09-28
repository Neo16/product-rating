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
            var baseQuery = context.Products
                .Include(e => e.ThumbnailPicture)             
                .Include(e => e.Brand)
                .Include(e => e.Category)               
                .ThenInclude(e => e.Parent)
                .ThenInclude(e => e.Parent)
                .Include(e => e.PropertyValueConnections)
                .ThenInclude(e => e.ProductAttributeValue)
                .ThenInclude(e => e.Attribute)
                .AsQueryable();

            //Text search filter
            if (!string.IsNullOrEmpty(filter.TextFilter))
            {
                baseQuery = baseQuery.Where(e => e.Name.ToUpper().Contains(filter.TextFilter.ToUpper())
                 || e.Category.Name.ToUpper().Contains(filter.TextFilter.ToUpper()));
            }

            // Add category brand and Price filters 
            var query = AddCategoryBrandPriceFilters(filter, baseQuery);

            var notFilteredForCategoryQuery = AddCategoryBrandPriceFilters(filter, baseQuery, true);
            var notFilteredForBrandQuery = AddCategoryBrandPriceFilters(filter, baseQuery, false, true, false);
            var notFilteredForPriceQuery = AddCategoryBrandPriceFilters(filter, baseQuery, false, false, true);

            //filter for attributes
            var attributeFilters = MapAttributeFilters(filter);
            query = FilterForAttributes(attributeFilters, query);

            var queryBeforeOrderAndPagination = query;

            //ordering 
            Expression<Func<Product, ValueType>> orderByExpression = null; 
            if (filter.OrderBy != null)
            {
                switch (filter.OrderBy.Value)
                {
                    case ProductOrder.BestScore:
                        orderByExpression = e => e.ScoreValue;
                        break;
                    case ProductOrder.Price:
                        orderByExpression = e => e.Price;
                        break;
                    case ProductOrder.MostTextReview:
                        orderByExpression = e => e.Reviews.Count;
                        break;
                    case ProductOrder.Newest:
                        orderByExpression = e => e.CreatedAt;
                        break;
                    default:
                        orderByExpression = e => e.ScoreValue;
                        break;
                }
            }
            query = filter.Order == Order.Asc
                ? query.OrderBy(orderByExpression)
                : query.OrderByDescending(orderByExpression);

            //pagination            
            if (pagination.Start != null && pagination.Length != null)
            {
                query = query.Skip(pagination.Start.Value - 1).Take(pagination.Length.Value);
            }

            var products = await query
                .ToListAsync();

            var maxPriceOption = (await notFilteredForPriceQuery.AnyAsync()) ? (await notFilteredForPriceQuery.MaxAsync(e => e.Price) + 100) : 1000;
            if (maxPriceOption == 0)
            {
                maxPriceOption = 1000;
            }

            var result = new SearchResultDto()
            {
                Products = products.Select(e => mapper.Map<ProductHeaderDto>(e)).ToList(),
                Brands = notFilteredForBrandQuery.Where(e => e.Brand != null).Select(e => e.Brand).Distinct().Select(e => mapper.Map<BrandHeaderDto>(e)).ToList(),
                Categories = GetRootCategories(notFilteredForCategoryQuery.Select(e => e.Category)).Select(e => mapper.Map<CategoryHeaderDto>(e)).ToList(),
                MaxPriceOption = maxPriceOption,
                TotalNumOfResults = await queryBeforeOrderAndPagination.CountAsync()
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

        public async Task<ProductDetailsDto> GetDetails(Guid productId, Guid? userId)
        {
            var product = await context.Products
                .Include(e => e.ThumbnailPicture)
                .Include(e => e.Category)
                .Include(e => e.Brand)
                .Include(e => e.PropertyValueConnections)
                .ThenInclude(e => e.ProductAttributeValue)
                .ThenInclude(e => e.Attribute)
                .Where(e => e.Id == productId)
                .FirstOrDefaultAsync();

            var productDo = mapper.Map<ProductDetailsDto>(product);

            if (userId != null)
            {
                productDo.ScoreByMe = await context.Scores
                  .Where(e => e.ProductId == productId)
                  .Where(e => e.AuthorId == userId.Value)
                  .Select(e => e.Value)
                  .FirstOrDefaultAsync();
            }         

            return productDo;
        }

        private List<Expression<Func<ProductAttributeValue, bool>>> MapAttributeFilters(ProductFilterDto filter)
        {
            List<Expression<Func<ProductAttributeValue, bool>>> filters = new List<Expression<Func<ProductAttributeValue, bool>>>();

            if (filter.StringAttributes != null)
            {
                foreach (StringAttribute stringFilterAttr in filter.StringAttributes)
                {
                    filters.Add(e => e is ProductAttributeStringValue && e.Attribute.Id == stringFilterAttr.AttributeId
                            && ((e as ProductAttributeStringValue).StringValue == stringFilterAttr.Value || e.Id == stringFilterAttr.ValueId));
                }
            }
            if (filter.IntAttributes != null)
            {
                foreach (IntAttribute intFilterAttr in filter.IntAttributes)
                {
                    filters.Add(e => e is ProductAttributeIntValue && e.Attribute.Id == intFilterAttr.AttributeId
                            && ((e as ProductAttributeIntValue).IntValue == intFilterAttr.Value || e.Id == intFilterAttr.ValueId));
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
        private static IQueryable<Product> FilterForBrands(List<Guid> brandIds, IQueryable<Product> query)
        {
            if (brandIds != null && brandIds.Count > 0)
            {
                query = query.Where(e => e.Brand == null || !brandIds.Contains(e.BrandId.Value));
            }
            return query;
        }

        private static IQueryable<Product> FilterForPrice(ProductFilterDto filter, IQueryable<Product> query)
        {

            if (filter.MinimumPrice != null && filter.MaximumPrice != null)
            {
                query = query.Where(e => e.Price >= filter.MinimumPrice && e.Price <= filter.MaximumPrice);
            }
            return query;
        }

        private static IQueryable<Product> AddCategoryBrandPriceFilters(
            ProductFilterDto filter,
            IQueryable<Product> query,
            bool skipCategory = false,
            bool skipBrand = false,
            bool skipPrice = false)
        {
            if (!skipCategory)
            {
                query = FilterForCategory(filter.CategoryId, query);
            }
            if (!skipBrand)
            {
                query = FilterForBrands(filter.BrandIds, query);
            }
            if (!skipPrice)
            {
                query = FilterForPrice(filter, query);
            }
            return query;
        }


        private static IQueryable<Product> FilterForCategory(Guid? categoryId, IQueryable<Product> query)
        {
            if (categoryId != null)
            {
                // three lvl deep
                query = query.Where(e =>
                      e.CategoryId == categoryId.Value
                   || e.Category.Parent.Id == categoryId.Value
                   || e.Category.Parent.Parent.Id == categoryId.Value);
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
                .FirstOrDefaultAsync(e => e.Id == productId);

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

            if (product.IntAttributes != null)
            {
                foreach (var attr in product.IntAttributes)
                {
                    if(attr.ValueId == null)
                    {
                        dbProduct.PropertyValueConnections.Add(new ProductAttributeValueConnection()
                        {
                            ProductAttributeValue = new ProductAttributeIntValue()
                            {
                                AttributeId = attr.AttributeId,
                                IntValue = attr.Value                               
                            }
                        });
                    }
                    else
                    {
                        dbProduct.PropertyValueConnections.Add(new ProductAttributeValueConnection()
                        {
                            ProductAttributeValueId = attr.ValueId.Value                           
                        });
                    }                    
                }
            }
          
            if(product.StringAttributes != null)
            {
                foreach (var attr in product.StringAttributes)
                {
                    if (attr.ValueId == null)
                    {
                        dbProduct.PropertyValueConnections.Add(new ProductAttributeValueConnection()
                        {
                            ProductAttributeValue = new ProductAttributeStringValue()
                            {
                                AttributeId = attr.AttributeId,
                                StringValue = attr.Value                              
                            }
                        });
                    }
                    else
                    {
                        dbProduct.PropertyValueConnections.Add(new ProductAttributeValueConnection()
                        {
                            ProductAttributeValueId = attr.ValueId.Value
                        });
                    }                    
                }
            }            

            context.Products.Add(dbProduct);
            await context.SaveChangesAsync();
            return dbProduct.Id;
        }

        public async Task UpdateProduct(Guid productId, CreateEditProductDto product)
        {
            var oldDbProduct = await context.Products
              .Include(e => e.PropertyValueConnections)
              .ThenInclude(e => e.ProductAttributeValue)             
              .SingleOrDefaultAsync(e => e.Id == productId);

            if (oldDbProduct == null)
            {
                throw new BusinessLogicException("The product does not exist.")
                {
                    ErrorCode = ErrorCode.InvalidArgument
                };
            }

            var newDbProduct = mapper.Map<Product>(product);

            //1. Kategória alap adatai frissíteni 
            oldDbProduct.Name = newDbProduct.Name;
            oldDbProduct.CategoryId = newDbProduct.CategoryId;
            oldDbProduct.BrandId = newDbProduct.BrandId;
            oldDbProduct.ThumbnailPictureId = newDbProduct.ThumbnailPictureId;
            oldDbProduct.StartOfProduction = newDbProduct.StartOfProduction;
            oldDbProduct.EndOfProduction = newDbProduct.EndOfProduction;


            //2.: Todo Attribútum értékek frissítése 

            await context.SaveChangesAsync();
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

            foreach (var connection in product.PropertyValueConnections)
            {
                context.Entry(connection).State = EntityState.Deleted;
                var attrValue = connection.ProductAttributeValue;

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

        public async Task<List<ProductManageHeaderDto>> AdminGetProducts(ManageProductFilterDto filter, Guid userId, PaginationDto pagination)
        {
            var query = context.Products
              .Include(e => e.Brand)
              .Include(e => e.Category)        
              .AsQueryable();

            query = FilterForCategory(filter.CategoryId, query);
            query = FilterForBrands(filter.BrandIds, query);

            if (!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(e => e.Name.ToUpper().Contains(filter.Name.ToUpper()));                
            }
            if (filter.IsMine == true)
            {
                query = query.Where(e => e.CreatorId == userId);
            }

            //pagination            
            if (pagination.Start != null && pagination.Length != null)
            {
                query = query.Skip(pagination.Start.Value - 1).Take(pagination.Length.Value);
            }

            return await query
                .ProjectTo<ProductManageHeaderDto>(mapperConfiguration)
                .ToListAsync();
        }
    }
}
