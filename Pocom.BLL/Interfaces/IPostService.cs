using Microsoft.AspNetCore.Identity;
using Pocom.BLL.Models;
using Pocom.BLL.Models.ViewModels;
using Pocom.DAL.Entities;

namespace Pocom.BLL.Interfaces
{
    public interface IPostService
    {
        public IEnumerable<PostDTO> Get(Func<Post, bool> predicate);
        public IEnumerable<PostDTO> GetAll();
        public IEnumerable<PostDTO> Get(RequestViewModel vm);
        public PostDTO? GetPost(Guid id);
        public Task<IdentityResult> Create(string email, PostDTO post);
        public void Update(PostDTO item);
        public void UpdateText(Guid id, Guid authorId, string text);
        public void Delete(Guid id);
        public IEnumerable<PostDTO> Sort(IQueryable<PostDTO> items, string props);
        Task<IEnumerable<PostDTO>> GetUserReactionsPosts(string email);
    }
}
