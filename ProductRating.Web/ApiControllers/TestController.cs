using Microsoft.AspNetCore.Mvc;
using ProductRating.Bll.Dtos;
using ProductRating.Bll.Dtos.Product;
using ProductRating.Bll.Dtos.Product.Attributes;
using ProductRating.Bll.ServiceInterfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductRating.Controllers
{
    [ApiController]
    [Route("Test")]
    public class TestController : Controller
    {
        private readonly IProductService productService;

        public TestController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var result = await productService.Find(new ProductFilterDto()
            {
                Attributes = new List<AttributeBase>()
                {
                    new StringAttribute(){AttributeName = "asd", Value = "Ez az érték" }
                }
            }, 
            new PaginationDto());
            return Ok();
        }
    }
}
