using ProductRating.Bll.Dtos;
using ProductRating.Bll.Dtos.Product;
using ProductRating.Model.Entities.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductRating.Bll.ServiceInterfaces
{
    public interface IProductService
    {
        Task<List<ProductHeaderDto>> Find(ProductFilterDto filter, PaginationDto pagination);

        Task<ProductDetailsDto> GetDetails(Guid productId);

    }
}
