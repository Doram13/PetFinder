using System;
using System.ComponentModel.DataAnnotations;

namespace PetFinder.Core.Models
{
    public class SeenDetail
    {
        public int Id { get; set; }
        public string Location { get; set; }
        [Required]
        public DateTime SeenTime { get; set; }
        public byte[] Map { get; set; }

        public SeenDetail()
        {
            SeenTime = DateTime.Now;
        }
    }
}
