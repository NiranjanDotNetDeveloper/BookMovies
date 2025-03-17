using Microsoft.AspNetCore.Mvc;

namespace BookingMovieUI.Controllers
{
    [Route("[Controller]")]
    public class CategoryController : Controller
    {
        [Route("AllCategory")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
