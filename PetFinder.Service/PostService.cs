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
        public async Task SavePostAsync(Post post)
        {
            //post.IsActive = true;   the Post's constructor sets IsActive to true Defaultly
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

        public async Task<List<Post>> GetAllActivePosts()
        {
            return await _context.Posts
                .Include(pet => pet.PostedPet)
                    .ThenInclude(sd => sd.SeenDetail)
                .Where(p => p.IsActive == true)
                .OrderByDescending(time => time.PostedPet.SeenDetail.SeenTime)
                .ToListAsync();
        }

        public async Task<Post> GetPostById(int id)
        {
            return await _context.Posts
                .Include(pet => pet.PostedPet)
                    .ThenInclude(sd => sd.SeenDetail)
                .Where(p => p.IsActive == true)
                .FirstOrDefaultAsync(post => post.Id == id);
        }

        public async Task<List<Post>> GetAllSeenPetPosts()
        {
            return await _context.Posts
                .Include(pet => pet.PostedPet)
                    .ThenInclude(sd => sd.SeenDetail)
                .Where(p => p.PostType == PostTypes.SEEN)
                .Where(p => p.IsActive == true)
                .OrderByDescending(time => time.PostedPet.SeenDetail.SeenTime)
                .ToListAsync();
        }

        public async Task<List<Post>> GetAllLostPetPosts()
        {
            return await _context.Posts
                .Include(pet => pet.PostedPet)
                    .ThenInclude(sd => sd.SeenDetail)
                .Where(p => p.PostType == PostTypes.LOST)
                .Where(p => p.IsActive == true)
                .OrderByDescending(time => time.PostedPet.SeenDetail.SeenTime)
                .ToListAsync();
        }

        public async Task<List<Post>> GetAllPosts()
        {
            return await _context.Posts
                .Include(pet => pet.PostedPet)
                    .ThenInclude(sd => sd.SeenDetail)
                .OrderByDescending(time => time.PostedPet.SeenDetail.SeenTime)
                .ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetAllPostWithSearchStringAsync(string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                return await _context.Posts
                .Where(post => post.Description.Contains(searchString) || post.Title.Contains(searchString))
                .Where(post => post.IsActive == true)
                .Include(pet => pet.PostedPet)
                    .ThenInclude(sd => sd.SeenDetail)
                .OrderByDescending(time => time.PostedPet.SeenDetail.SeenTime)
                .ToListAsync();
            }

            return null;
        }

        public async Task<bool> UpdatePostEntryAsync(Post post)
        {
            try
            {
                _context.Update(post);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(post.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }

        public async Task DeleteAsync(Post post)
        {
            try
            {
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to update in database: {ex.Message}");
            }
        }
    }
}
