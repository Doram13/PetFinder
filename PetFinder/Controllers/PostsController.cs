using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetFinder.Core;
using PetFinder.Core.Models;
using System;

namespace PetFinder.Controllers
{
    public class PostsController : Controller
    {

        private readonly IPost _postService;

        public PostsController(IPost postservice)
        {
            _postService = postservice;

        }

        public async Task<IActionResult> SeenPets()
        {
            return View(await _postService.GetAllSeenPetPosts());
        }

        public async Task<IActionResult> LostPets()
        {
            return View(await _postService.GetAllLostPetPosts());
        }


        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _postService.GetPostById((int)id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _postService.GetPostById((int)id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id, IsActive, PostType, PostedPet, User, Title, PostDate, Description")] Post post)
        {
            if (Int32.Parse(id) != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (await _postService.UpdatePostEntryAsync(post))
                {
                    if (post.PostType == PostTypes.LOST)
                    {
                        return RedirectToAction(nameof(LostPets));
                    }
                    else
                    {
                        return RedirectToAction(nameof(SeenPets));
                    }
                }
            }
            return View(post);
        }

        public IActionResult Index()
        {
            // return to HomeController as PostController does not have an IndexPage
            HomeController hc = new HomeController(_postService);
            return hc.Index();
        }

        public IActionResult CreatePost()
        {
            var post = new Post(true);
            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveNewPost(Post post)
        {
            await _postService.SavePostAsync(post);
            if (post.PostType == PostTypes.LOST)
            {
                return RedirectToAction(nameof(LostPets));
            }
            else
            {
                return RedirectToAction(nameof(SeenPets));
            }
        }
    }
}