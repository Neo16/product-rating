using ProductRating.Bll.Dtos;
using ProductRating.Bll.Dtos.Brand;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductRating.Bll.ServiceInterfaces
{
    public interface IBrandService
    {
        Task<List<BrandManageHeaderDto>> AdminGetBrands(ManageBrandFilterDto filter, Guid userId, PaginationDto pagination);
           
        Task<CreateEditBrandDto> GetBrandForUpdate(Guid brandId);

        Task<Guid> CreateBrand(CreateEditBrandDto brand, Guid creatorId);

        Task UpdateBrand(Guid brandId, CreateEditBrandDto brand);

        Task DeleteBrand(Guid brandId);
    }
}
