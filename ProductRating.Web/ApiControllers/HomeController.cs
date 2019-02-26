using Microsoft.AspNetCore.Mvc;
using ProductRating.Bll.Dtos.Product;
using ProductRating.Bll.Dtos.Product.Attributes;
using ProductRating.Bll.ServiceInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductRating.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService productService;

        public HomeController(IProductService productService)
        {
            this.productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await productService.Test(new ProductFilter()
            {
                StringAttributeFilters = new List<StringAttribute>()
                {
                    new StringAttribute(){AttributeName = "asd", Value = "Ez az érték" }
                }
            });
            return Ok();
        }
    }
}
