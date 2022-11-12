using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pocom.DAL.Entities
{
    public class UserAccount : IdentityUser
    {
        //Guid Id { get; set; }
        public string Name { get; set; }
        public List<Post>? Posts { get; set; } 
        public List<Reaction>? Reactions { get; set; }
        public List<Comment>? Comments { get; set; }
    }
}
