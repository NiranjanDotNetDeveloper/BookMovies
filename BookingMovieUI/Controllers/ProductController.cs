using Microsoft.AspNetCore.Mvc;

namespace BookingMovieUI.Controllers
{
    [Route("[Controller]")]
    public class ProductController : Controller
    {
        [Route("AllBooks")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
