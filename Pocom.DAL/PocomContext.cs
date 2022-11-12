using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pocom.DAL.Entities;
using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pocom.DAL
{
    public class PocomContext : IdentityDbContext<UserAccount>
    {
        public PocomContext(DbContextOptions<PocomContext> options) : base(options) {
            Database.EnsureCreated();
        }

        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Reaction> Reactions { get; set; }
    }
}
