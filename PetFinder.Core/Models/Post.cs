using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetFinder.Core.Models
{
    public class Post
    {
        public int Id { get; set; }
        public PostTypes PostType { get; set; }
        public DateTime PostDate { get; set; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }
        public Pet PostedPet { get; set; }
        public string Description { get; set; }
    }
}
