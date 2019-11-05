using ProductRating.Bll.Dtos;
using ProductRating.Bll.Dtos.Product;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductRating.Bll.ServiceInterfaces
{
    public interface IProductService
    {
        Task<SearchResultDto> Find(ProductFilterDto filter, PaginationDto pagination);

        Task<ProductDetailsDto> GetDetails(Guid productId, Guid? userId);

        Task<CreateEditProductDto> GetProductForUpdate(Guid productId);

        Task<Guid> CreateProduct(CreateEditProductDto product);

        Task UpdateProduct(Guid productId, CreateEditProductDto product, Guid userId, bool IsAdmin);

        Task DeleteProduct(Guid productId, Guid userId, bool IsAdmin);

        Task AddOffer(Guid userId, Guid productId, CreateEditOfferDto offer);

        Task DeleteOffer(Guid userId, Guid productId);

        Task<List<OfferHeaderDto>> ListOffers(Guid productId);

        Task<List<ProductManageHeaderDto>> AdminGetProducts(ManageProductFilterDto filter, Guid UserId, PaginationDto pagination);

        Task<OfferHeaderDto> GetOfferForProduct(Guid userId, Guid productId);

        Task<List<ProductHeaderDto>> ListFirstTen();
    }
}
