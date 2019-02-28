using Microsoft.AspNetCore.Mvc;
using ProductRating.Bll.Dtos.Product;
using ProductRating.Bll.Dtos.Product.Attributes;
using ProductRating.Bll.ServiceInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductRating.Controllers
{
    [ApiController]
    [Route("Test")]
    public class HomeController : Controller
    {
        private readonly IProductService productService;

        public HomeController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var result = await productService.Test(new ProductFilter()
            {
                Attributes = new List<AttributeBase>()
                {
                    new StringAttribute(){AttributeName = "asd", Value = "Ez az érték" }
                }
            });
            return Ok();
        }
    }
}
