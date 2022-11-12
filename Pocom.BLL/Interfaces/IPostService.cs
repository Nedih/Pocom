using Pocom.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pocom.BLL.Interfaces
{
    internal interface IPostService
    {
        public IEnumerable<Post> GetAsync(Func<Post, bool> predicate);
        public Post FirstOrDefaultAsync(Func<Post, bool> predicate);
        public void CreateAsync(Post item);
        public void UpdateAsync(Post item);
        public void DeleteAsync(string id);

    }
}
