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
                Console.WriteLine("Hiba");
                Console.WriteLine(ex.Message);
            }
        }

        public Task DeletePost(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetAllActivePosts()
        {
            return _context.Posts;
        }

        public IEnumerable<Post> GetAllPosts()
        {
            throw new NotImplementedException();
        }

        public string GetDescription(int id)
        {
            throw new NotImplementedException();
        }

        public bool GetIsPostActive(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Post> GetPostById(int id)
        {
            return await _context.Posts.FirstOrDefaultAsync(post => post.Id == id);
        }

        public DateTime GetPostDateTime(int id)
        {
            throw new NotImplementedException();
        }

        public Microsoft.AspNetCore.Identity.IdentityUser GetPostOwner(int postId)
        {
            throw new NotImplementedException();
        }

        public Pet GetPostPet(int postId)
        {
            throw new NotImplementedException();
        }

        public string GetPostTitle(int id)
        {
            throw new NotImplementedException();
        }

        public PostTypes GetPostType(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePostDescription(int postId, string newDescription)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePostTitle(int postId, string newTitle)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Post>> GetAllSeenPetPosts()
        {
            return await _context.Posts.Where(p => p.PostType == PostTypes.SEEN).ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetAllLostPetPosts()
        {
            return await _context.Posts.Where(p => p.PostType == PostTypes.LOST).ToListAsync();
        }
    }
}
