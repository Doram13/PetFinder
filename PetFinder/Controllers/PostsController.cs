using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetFinder.Core;
using PetFinder.Core.Models;

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

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,PostType,PostDate,IsActive,Description,Title")] Post post)
        //{
        //    if (id != post.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(post);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!PostExists(post.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(SeenPets));
        //    }
        //    return View(post);
        //}

        //private bool PostExists(int id)
        //{
        //    return _context.Posts.Any(e => e.Id == id);
        //}

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreatePost()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveNewPost(Post post)
        {
            await _postService.SavePostAsync(post);
            return RedirectToAction(nameof(SeenPets));
        }
    }
}