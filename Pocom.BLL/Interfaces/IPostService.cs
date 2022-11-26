using Microsoft.AspNetCore.Identity;
using Pocom.BLL.Models;
using Pocom.BLL.Models.ViewModels;
using Pocom.DAL.Entities;

namespace Pocom.BLL.Interfaces
{
    public interface IPostService
    {
        public IEnumerable<PostDTO> GetAsync(Func<Post, bool> predicate);
        public IEnumerable<PostDTO> GetAsync(RequestViewModel vm);
        public PostDTO FirstOrDefaultAsync(Func<Post, bool> predicate);
        public Task<IdentityResult> CreateAsync(string email, PostDTO post);
        public void UpdateAsync(PostDTO item);
        public void UpdateTextAsync(Guid id, Guid authorId, string text);
        public void DeleteAsync(Guid id);
        public IEnumerable<PostDTO> Sort(IQueryable<PostDTO> items, string props);

    }
}
