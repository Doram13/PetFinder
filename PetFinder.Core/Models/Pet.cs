using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PetFinder.Core.Models
{
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        public AnimalTypes AnimalType { get; set; }
        public string PicturePath { get; set; }
        [Required]
        public Dictionary<Tag, bool> Tags { get; set; }
        public virtual SeenDetail SeenDetail { get; set; }

        public Pet() {}

        public Pet(bool newPet)
        {
            Tags = new Dictionary<Tag, bool>();
            SeenDetail = new SeenDetail(true);

            foreach (Tag tag in (Tag[])Enum.GetValues(typeof(Tag)))
            {
                Tags.Add(tag, false);
            }
        }
    }
}
