using Microsoft.AspNetCore.Mvc;
using ProductRating.Bll.Dtos;
using ProductRating.Bll.Dtos.Category;
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
        private readonly ICategoryService categoryService;

        public TestController(
            IProductService productService,
             ICategoryService categoryService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
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

           await categoryService.CreateCategory(new CreateCategoryDto()
            {
               Name = "Játékok",
               Attributes = new List<AttributeBase>()
               {
                   new StringAttribute() {AttributeName = "Méret"}
               }
            });
            
            return Ok();
        }
    }
}
