using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductRating.Bll.Dtos;
using ProductRating.Bll.Dtos.Brand;
using ProductRating.Bll.Exceptions;
using ProductRating.Bll.ServiceInterfaces;
using ProductRating.Dal;
using ProductRating.Dal.Model.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductRating.Bll.Services
{
    public class BrandService : ServiceBase, IBrandService
    {
        private readonly IMapper mapper;
        private readonly MapperConfiguration mapperConfiguration;

        public BrandService(
            ApplicationDbContext context,
            IMapper mapper,
            MapperConfiguration mapperConfiguration
            ) : base(context)
        {
            this.mapperConfiguration = mapperConfiguration;
            this.mapper = mapper;
        }

        public async Task<List<BrandManageHeaderDto>> AdminGetBrands(ManageBrandFilterDto filter, Guid userId, PaginationDto pagination)
        {
            var query = context.Brands                
                 .Include(e => e.Products)       
                 .ThenInclude(f => f.Category)
                 .AsQueryable();

            //filter
            if (!string.IsNullOrWhiteSpace(filter.Name))
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

            // ProjectTo doesnt work for this mapping (has string.join)
            var brands = await query.ToListAsync();

            return brands
                .Select(e => mapper.Map<BrandManageHeaderDto>(e))
                .Select(e => { e.IsCreatedByMe = e.CreatorId == userId; return e; })
                .ToList();
        }

        public async Task<Guid> CreateBrand(CreateEditBrandDto brand, Guid creatorId)
        {
            var dbBrand = new Brand() 
            {
                Name = brand.Name,
                CreatorId = creatorId
            };

            bool isBrandNameTaken = await context.Brands
                .AnyAsync(e => e.Name.ToUpper() == dbBrand.Name.ToUpper());

            if (isBrandNameTaken)
            {
                throw new BusinessLogicException("A brand with the same name already exists.")
                {
                    ErrorCode = ErrorCode.InvalidArgument
                };
            }

            context.Brands.Add(dbBrand);
            await context.SaveChangesAsync();

            return dbBrand.Id;
        }

        public async Task DeleteBrand(Guid brandId)
        {
            bool hasProducts = await context.Brands
                .Where(e => e.Id == brandId)
                .Where(e => e.Products.Count > 0)
                .AnyAsync();

            if (hasProducts)
            {
                throw new BusinessLogicException(
                    "The brand can not be deleted, because there are still products in this brand.")
                {
                    ErrorCode = ErrorCode.InvalidArgument
                };
            }           

            var brand = await context.Brands            
              .SingleOrDefaultAsync(e => e.Id == brandId);
            
            if (brand != null)
            {
                context.Brands.Remove(brand);
                await context.SaveChangesAsync();
            }           
        }

        public async Task<CreateEditBrandDto> GetBrandForUpdate(Guid brandId)
        {
            var dbBrand = await context.Brands               
                .FirstOrDefaultAsync(e => e.Id == brandId);
            var mappedCategory = mapper.Map<CreateEditBrandDto>(dbBrand);        

            return mappedCategory;
        }

        public async Task UpdateBrand(Guid brandId, CreateEditBrandDto brand)
        {
            var oldDbBrand = await context.Brands             
                .SingleOrDefaultAsync(e => e.Id == brandId);

            if (oldDbBrand == null)
            {
                throw new BusinessLogicException("The brand does not exist.")
                {
                    ErrorCode = ErrorCode.InvalidArgument
                };
            }

            oldDbBrand.Name = brand.Name;
            await context.SaveChangesAsync();
        }
    }
}
