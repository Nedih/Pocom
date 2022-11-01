﻿using Pocom.BLL.Interfaces;
using Pocom.DAL.Entities;
using Pocom.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pocom.BLL.Services
{
    internal class PostService : IPostService
    {
        private readonly IRepository<Post> _repository;

        public PostService(IRepository<Post> repository)
        {
            _repository = repository;
        }
        public void CreateAsync(Post item)
        {
            _repository.AddAndSave(item);
        }

        public void DeleteAsync(string id)
        {
            _repository.RemoveAndSave(id);
        }

        public Post FirstOrDefaultAsync(Func<Post, bool> predicate)
        {
            return _repository.FirstOrDefault(predicate);
        }

        public IEnumerable<Post> GetAsync(Func<Post, bool> predicate)
        {
            return _repository.GetWhere(predicate);
        }

        public void UpdateAsync(Post item)
        {
            _repository.UpdateAndSave(item);
        }
    }
}
