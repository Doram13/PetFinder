using Microsoft.AspNetCore.Identity;
using PetFinder.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PetFinder.Core
{
    public interface IPost
    {
        Post GetPostById(int id);
        IEnumerable<Post> GetAllPosts();
        IEnumerable<Post> GetAllActivePosts();
        string GetPostTitle(int id);
        PostTypes GetPostType(int id);
        DateTime GetPostDateTime(int id);
        bool GetIsPostActive(int id);
        IdentityUser GetPostOwner(int postId);
        Pet GetPostPet(int postId);
        string GetDescription(int id);

        Task CreatePost(Post post);
        Task DeletePost(int id);
        Task UpdatePostDescription(int postId, string newDescription);
        Task UpdatePostTitle(int postId, string newTitle);
    }
}
