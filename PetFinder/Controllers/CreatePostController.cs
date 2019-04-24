using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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