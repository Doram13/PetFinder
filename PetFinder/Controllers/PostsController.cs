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
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _postService.GetPostById(id);
            if (post == null)
            {
                return NotFound();
            }
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", post.UserId);
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
            Post postToChange = await _postService.GetPostById(Int32.Parse(id));
            
            post.Id = postToChange.Id + 100;
            post.IsActive = postToChange.IsActive;
            post.PostType = postToChange.PostType;
            post.PostedPet = postToChange.PostedPet;
            post.User = postToChange.User;


            if (ModelState.IsValid)
            {
                try
                {
                    await _postService.EditPostContentAsync(post, postToChange);
                    if (post.PostType.Equals("SEEN"))
                    {
                        return RedirectToAction(nameof(SeenPets));
                    }
                    else return RedirectToAction(nameof(LostPets));
                }
               catch (DbUpdateConcurrencyException ex)
                {
                   Console.WriteLine($"Failed to save to database: {ex.Message}");
                }
               catch (Exception ex)
                {
                   Console.WriteLine($"Failed to save to database: {ex.Message}");
                }
                //return RedirectToAction(nameof(Index));
            }
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", post.UserId);
            return View(post);
        }

        //POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("PostDate,Description,Title")] Post post)
        //{
        //    if (id != post.Id)
        //    {
        //        return NotFound();
        //    }
        //
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            await _postService.EditPostContentAsync(id, post);
        //            await _postService.SavePostAsync(post);
        //        }
        //        catch (DbUpdateConcurrencyException ex)
        //        {
        //            Console.WriteLine($"Failed to save to database: {ex.Message}");
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine($"Failed to save to database: {ex.Message}");
        //        }
        //    }
        //    return View(post);
        //}




        //private bool PostExists(int id)
        //{
        //  return DbContext.post.Any(e => e.Id == id);
        //}

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreatePost()
        {
            var post = new Post();
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