using Microsoft.AspNetCore.Mvc;
using ProductRating.Bll.Dtos.Category;
using ProductRating.Bll.Dtos.Category.CategoryAttributes;
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
            //var result = await productService.Find(new ProductFilterDto()
            //{
            //    StringAttributes = new List<StringAttribute>()
            //    {
            //        new StringAttribute(){AttributeName = "asd", Value = "Ez az érték" }
            //    }
            //}, 
            //new PaginationDto());

            await categoryService.CreateCategory(new CreateEditCategoryDto()
            {
                Name = "Játékok",
                Attributes = new List<CreateEditCategoryAttributeDto>()
               {
                   new CreateEditCategoryAttributeDto()
                   {
                       AttributeName = "Méret",
                       Type = AttributeType.Int,
                       HasFixedValues = false
                   },
                   new CreateEditCategoryAttributeDto()
                   {
                       AttributeName = "Izé",
                       Type = AttributeType.String,
                       HasFixedValues = true,
                       Values = new List<CreateEditCategoryAttributeValueDto>()
                       {
                           new CreateEditCategoryAttributeValueDto()
                           {
                               StringValue = "Kettő"
                           }
                       }
                   }
               }
            });

            return Ok();
        }
    }
}
