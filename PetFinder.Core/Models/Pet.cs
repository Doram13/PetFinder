using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetFinder.Core.Models
{
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AnimalTypes AnimalType { get; set; }
        public string Picture { get; set; }
        public SeenDetails SeenDetail { get; set; }
        public List<Tag> tags { get; set; }

    }
}
