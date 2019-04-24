using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetFinder.Core.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string email { get; set; }
        public string Password { get; set; }
        public List<Post> Posts { get; set; }

    }
}
