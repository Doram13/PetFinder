using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetFinder.Core.Models
{
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AnimalTypes AnimalType { get; set; }
        public string PicturePath { get; set; }
        public virtual SeenDetail SeenDetail { get; set; }
        [NotMapped]
        public IEnumerable<Tag> Tags { get; set; }

    }
}
