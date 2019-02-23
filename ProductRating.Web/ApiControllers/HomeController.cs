using Microsoft.AspNetCore.Mvc;
using ProductRating.Bll.ServiceInterfaces;

namespace ProductRating.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService productService;

        public HomeController(IProductService productService)
        {
            this.productService = productService;
        }

        public IActionResult Index()
        {
            productService.Test();
            return Ok();
        }   
    }
}
