using Microsoft.EntityFrameworkCore;
using PetFinder.Core;
using PetFinder.Core.Models;
using PetFinder.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetFinder.Service
{
    public class PostService : IPost
    {
        private readonly ApplicationDbContext _context;

        public PostService(ApplicationDbContext context)
        {
            _context = context;
        }
        public virtual async Task SavePostAsync(Post post)
        {
            post.IsActive = true;
            _context.Posts.Add(post);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save to database: {ex.Message}");
            }
        }

        public List<Post> GetAllActivePosts()
        {
            return _context.Posts
                .Where(p => p.IsActive == true)
                .ToList();
        }

        public async Task<Post> GetPostById(int id)
        {
            return await _context.Posts
                .Include(pet => pet.PostedPet)
                    .ThenInclude(sd => sd.SeenDetail)
                .FirstOrDefaultAsync(post => post.Id == id);
        }

        public async Task<List<Post>> GetAllSeenPetPosts()
        {
            return await _context.Posts
                .Where(p => p.PostType == PostTypes.SEEN)
                .ToListAsync();
        }

        public async Task<List<Post>> GetAllLostPetPosts()
        {
            return await _context.Posts
                .Where(p => p.PostType == PostTypes.LOST)
                .ToListAsync();
        }

        public List<Post> GetAllPosts()
        {
            return _context.Posts.ToList();
        }

        public async Task SetInactiveAsync(int id)
        {
            Post PostToSetInactive = await _context.Posts.FirstOrDefaultAsync(post => post.Id == id);
            PostToSetInactive.IsActive = false;
            _context.Posts.Update(PostToSetInactive);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to update in database: {ex.Message}");
            }
        }

        public async Task EditPostContentAsync(Post post, Post postToChange)
        {

            try
            {
                _context.Posts.Remove(postToChange);
                post.Id -= 100;
                _context.Posts.Add(post);
                
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine($"Failed to update in database: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to update in database: {ex.Message}");
            }
        }



        public async Task<IEnumerable<Post>> GetAllPostWithSearchString(string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                return await _context.Posts
                .Where(post => post.Description.Contains(searchString) || post.Title.Contains(searchString))
                .Where(post => post.IsActive == true)
                .Include(post => post.PostedPet)
                .ToListAsync();
            }

            throw new ArgumentException();
        }
    }
}
