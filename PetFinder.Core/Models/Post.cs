using Microsoft.AspNetCore.Identity;
using System;

namespace PetFinder.Core.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public PostTypes PostType { get; set; }
        public DateTime PostDate { get; set; }
        public bool IsActive { get; set; }
        public virtual IdentityUser User { get; set; }
        public virtual Pet PostedPet { get; set; }
        public string Description { get; set; }
    }
}
