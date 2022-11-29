using Microsoft.AspNetCore.Identity;
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
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Reaction> Reactions { get; set; }
        public PocomContext(DbContextOptions<PocomContext> options) : base(options) {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserAccount>().HasMany(a => a.Posts).WithOne(p => p.Author).OnDelete(DeleteBehavior.ClientCascade);
            modelBuilder.Entity<UserAccount>().HasMany(a => a.Reactions).WithOne(p => p.Author).OnDelete(DeleteBehavior.ClientCascade);
            modelBuilder.Entity<Post>().HasMany(a => a.Reactions).WithOne(p => p.Post).OnDelete(DeleteBehavior.ClientCascade);
            //modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "User", NormalizedName = "USER", Id = "1c1ebabd-6745-4fc2-808d-48df8107736c", ConcurrencyStamp = "538a039d-be6c-4c60-8b0f-f6cb614cbc26" });
            //modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Admin", NormalizedName = "ADMIN", Id = "47e17bad-e591-4084-b31c-40c1e4859bd7", ConcurrencyStamp = "96a1ac22-fe97-4ee4-b4e1-937162ce4c57" });
        }
    }
}
