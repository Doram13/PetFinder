using System;
using System.Collections.Generic;


namespace PetFinder.Core.Models
{
    public class PostWithTag
    {
        public Post Post { get; set; }
        public Dictionary<Tag, bool> Tags { get; set; }

        public PostWithTag()
        {
            foreach (Tag tag in (Tag[])Enum.GetValues(typeof(Tag)))
            {
                Tags[tag] = false;
            }
        }

    }
}
