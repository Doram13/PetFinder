using PetFinder.Core;
using PetFinder.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetFinder.Service
{
    public class PostService : IPost
    {
        public Task CreatePost(Post post)
        {
            throw new NotImplementedException();
        }

        public Task DeletePost(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetAllActivePosts()
        {
            throw new NotImplementedException();
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

        public Post GetPostById(int id)
        {
            throw new NotImplementedException();
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
    }
}
