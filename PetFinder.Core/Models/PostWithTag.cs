using System;
using System.Collections.Generic;


namespace PetFinder.Core.Models
{
    public class PostWithTag
    {
        public Post Post { get; set; }
        public Dictionary<Tag, bool> Tags = new Dictionary<Tag, bool>();

        public PostWithTag()
        {
            foreach (Tag tag in (Tag[])Enum.GetValues(typeof(Tag)))
            {
                Tags.Add(tag, false);
            }
        }

    }
}
