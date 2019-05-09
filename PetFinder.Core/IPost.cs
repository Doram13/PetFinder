using PetFinder.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetFinder.Core
{
    public interface IPost
    {
        Task<Post> GetPostById(int id);
        List<Post> GetAllPosts();
        List<Post> GetAllActivePosts();
        Task<List<Post>> GetAllSeenPetPosts();
        Task<List<Post>> GetAllLostPetPosts();
        Task SavePostAsync(Post post);
        Task SetInactiveAsync(int id);
        Task<IEnumerable<Post>> GetAllPostWithSearchString(string searchString);
        Task EditPostContentAsync(Post post, Post postToChange);
    }
}
