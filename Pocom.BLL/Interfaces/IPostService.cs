﻿using Microsoft.AspNetCore.Identity;
using Pocom.BLL.Models;
using Pocom.BLL.Models.ViewModels;
using Pocom.DAL.Entities;

namespace Pocom.BLL.Interfaces
{
    public interface IPostService
    {
        public Task<IEnumerable<PostDTO>> GetAllAsync(string? email = null);
        public IEnumerable<PostDTO> Get(RequestViewModel vm);
        public Task<PostDTO?> GetPostAsync(Guid id);
        public IEnumerable<PostDTO> GetComments(Guid id);
        public Task<IdentityResult> CreateAsync(string email, PostDTO post);
        public void Update(PostDTO item);
        public void UpdateText(Guid id, Guid authorId, string text);
        public void Delete(Guid id);
        public Task<IEnumerable<PostDTO>> GetUserReactionsPostsAsync(string email);
    }
}
