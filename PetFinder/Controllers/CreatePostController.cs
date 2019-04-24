using Microsoft.AspNetCore.Mvc;

namespace PetFinder.Controllers
{
    public class CreatePostController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}