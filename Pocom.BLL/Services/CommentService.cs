using Pocom.BLL.Interfaces;
using Pocom.DAL.Entities;
using Pocom.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Pocom.BLL.Services
{
    public class CommentService : ICommentService
    {
        private readonly IRepository<Comment> _repository;
        public CommentService(IRepository<Comment> repository)
        {
            _repository = repository;
        }

        public void CreateAsync(Comment item)
        {
            _repository.AddAndSave(item);
        }

        public void DeleteAsync(string id)
        {
            var entity = _repository.FirstOrDefault(x=>x.Id.ToString()==id);
            if (entity != null)
                _repository.RemoveAndSave(entity);
        }

        public IEnumerable<Comment> GetAsync(Func<Comment, bool> predicate)
        {
           return _repository.GetWhere(predicate);
        }

        public Comment FirstOrDefaultAsync(Func<Comment, bool> predicate)
        {
            return _repository.FirstOrDefault(predicate);
        }

        public void UpdateAsync(Comment item)
        {
            _repository.UpdateAndSave(item);
        }
    }
}
