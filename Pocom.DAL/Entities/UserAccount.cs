﻿using Microsoft.AspNetCore.Identity;
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
        [Required]
        public string Name { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string DateOfBirth { get; set; }
        //public string PhoneNumber { get; set; }
        public string? Image { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public virtual List<UserAccount>? Subscribers { get; set; }
        public virtual List<Post>? Posts { get; set; } 
        public virtual List<Reaction>? Reactions { get; set; }
    }
}
