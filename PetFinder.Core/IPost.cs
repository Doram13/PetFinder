using PetFinder.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetFinder.Core
{
    public interface IPost
    {
        Task<Post> GetPostById(int id);
        Task<List<Post>> GetAllPosts();
        Task<List<Post>> GetAllActivePosts();
        Task<List<Post>> GetAllSeenPetPosts();
        Task<List<Post>> GetAllLostPetPosts();
        Task SavePostAsync(Post post);
        Task<IEnumerable<Post>> GetAllPostWithSearchStringAsync(string searchString);
        Task<bool> UpdatePostEntryAsync(Post post);
        Task DeleteAsync(Post post);

    }
}
