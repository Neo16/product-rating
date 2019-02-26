using ProductRating.Bll.Dtos.Product;
using ProductRating.Model.Entities.Product;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductRating.Bll.ServiceInterfaces
{
    public interface IProductService
    {
        Task<List<Product>> Test(ProductFilter filter);
    }
}
