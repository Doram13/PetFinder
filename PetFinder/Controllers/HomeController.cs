﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PetFinder.Data;
using PetFinder.Models;

namespace PetFinder.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

    /*    public async Task<IActionResult> SeenPets()
        {
            return View(await _context.Posts.Where(p => p.PostType == PostTypes.SEEN).ToListAsync());
        }

        public async Task<IActionResult> LostPets()
        {
            return View(await _context.Posts.Where(p => p.PostType == PostTypes.LOST).ToListAsync());
        }
        */
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
