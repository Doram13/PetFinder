using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace PetFinder.Core.Models
{
    public class Post
    {
        
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public PostTypes PostType { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime PostDate { get; set; }


        public bool IsActive { get; set; } = true;

        [Required]
        public Pet PostedPet { get; set; }

        public virtual IdentityUser User { get; set; }

        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public Post()
        {
            PostDate = DateTime.Now;
            //IsActive = true;
            PostedPet = new Pet();
        }

    }
}
