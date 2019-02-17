using Microsoft.AspNetCore.Mvc;

namespace ProductRating.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Ok();
        }   
    }
}
