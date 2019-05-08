﻿using System;
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
        public Dictionary<Tag, bool> Tags;
        public virtual SeenDetail SeenDetail { get; set; }
        //[NotMapped]
        //public IEnumerable<Tag> Tags { get; set; }

        public Pet(string name="")
        {
            Tags = new Dictionary<Tag, bool>();
            SeenDetail = new SeenDetail();
            Name = name;

            foreach (Tag tag in (Tag[])Enum.GetValues(typeof(Tag)))
            {
                Tags.Add(tag, false);
            }
        }
    }
}
