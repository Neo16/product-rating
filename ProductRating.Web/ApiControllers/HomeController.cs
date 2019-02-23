using Microsoft.AspNetCore.Mvc;
using ProductRating.Bll.ServiceInterfaces;
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
            var result = await productService.Test();
            return Ok();
        }   
    }
}
