using Pocom.BLL.Interfaces;
using Pocom.DAL.Entities;
using Pocom.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pocom.BLL.Extensions;
using System.Security.Cryptography.X509Certificates;

namespace Pocom.BLL.Services
{
    public class PostService : IPostService
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

        public void DeleteAsync(Guid id)
        {
            var entity = _repository.FirstOrDefault(x => x.Id == id);
            if (entity != null)
                _repository.RemoveAndSave(entity);
        }

        public Post? FirstOrDefaultAsync(Func<Post, bool> predicate)
        {
            return _repository.Include(x => x.Author).FirstOrDefault(predicate);
        }

        public IQueryable<Post> GetAsync(Func<Post, bool> predicate)
        {
            return _repository.Include(x => x.Author).Where(predicate).AsQueryable<Post>();
        }

        public void UpdateAsync(Post item)
        {
            _repository.UpdateAndSave(item);
        }
        public IQueryable<Post> Sort(IQueryable<Post> items,string props)
        {
            if (props==null)
            {
                return items;
            }
            var sortByArray = props.Split(',');
            for (int i = 0; i < sortByArray.Length; i++)
            {
                items = i == 0
                    ? items.OrderBy(sortByArray[i].Replace(" desc", ""), sortByArray[i].EndsWith("desc"))
                    : items.ThenBy(sortByArray[i].Replace(" desc", ""), sortByArray[i].EndsWith("desc"));
            }
            return items;
        }

    }
}
