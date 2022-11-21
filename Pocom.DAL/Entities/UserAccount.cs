using Microsoft.AspNetCore.Identity;
using Pocom.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pocom.DAL.Entities
{
    public class UserAccount : IdentityUser
    {
        //Guid Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public List<Post>? Posts { get; set; } 
        public List<Reaction>? Reactions { get; set; }
        public List<Comment>? Comments { get; set; }
    }
}
