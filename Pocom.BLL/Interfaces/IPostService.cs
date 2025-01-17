using Microsoft.AspNetCore.Identity;
using Pocom.BLL.Models;
using Pocom.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Pocom.BLL.Interfaces
{
    public interface IPostService
    {
        public IQueryable<PostDTO> GetAsync(Func<Post, bool> predicate);
        public PostDTO FirstOrDefaultAsync(Func<Post, bool> predicate);
        public Task<IdentityResult> CreateAsync(string email, PostDTO post);
        public void UpdateAsync(Post item);
        public void DeleteAsync(Guid id);
        public IQueryable<PostDTO> Sort(IQueryable<PostDTO> items, string props);

    }
}
