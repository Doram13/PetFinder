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
            post.IsActive = true;
            _context.Posts.Add(post);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save to database: {ex.Message}" );
            }
        }

        public IEnumerable<Post> GetAllActivePosts()
        {
            return _context.Posts;
        }

        public async Task<Post> GetPostById(int id)
        {
            return await _context.Posts.FirstOrDefaultAsync(post => post.Id == id);
        }

        public async Task<IEnumerable<Post>> GetAllSeenPetPosts()
        {
            return await _context.Posts.Where(p => p.PostType == PostTypes.SEEN).ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetAllLostPetPosts()
        {
            return await _context.Posts.Where(p => p.PostType == PostTypes.LOST).ToListAsync();
        }

        public IEnumerable<Post> GetAllPosts()
        {
            throw new NotImplementedException();
        }

        public async Task SetInactiveAsync(int id)
        {
            var PostToSetInactive = await _context.Posts.FirstOrDefaultAsync(post => post.Id == id);
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

        public async Task EditPostContentAsync(int id, string newContent)
        {
            var PostToSetInactive = await _context.Posts.FirstOrDefaultAsync(post => post.Id == id);
            PostToSetInactive.IsActive = false;

            try
            {
                _context.Update(post);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(post.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

    }
}
