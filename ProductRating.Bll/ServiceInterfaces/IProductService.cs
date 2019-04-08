using ProductRating.Bll.Dtos;
using ProductRating.Bll.Dtos.Product;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductRating.Bll.ServiceInterfaces
{
    public interface IProductService
    {
        Task<List<ProductHeaderDto>> Find(ProductFilterDto filter, PaginationDto pagination);

        Task<ProductDetailsDto> GetDetails(Guid productId);

        Task<CreateEditProductDto> GetProductForUpdate(Guid productId);

        Task<Guid> CreateProduct(CreateEditProductDto product);

        Task UpdateProduct(Guid productId, CreateEditProductDto product);

        Task DeleteProduct(Guid productId);
    }
}
