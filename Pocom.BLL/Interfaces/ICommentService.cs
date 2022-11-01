using Pocom.DAL.Entities;
using Pocom.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pocom.BLL.Interfaces
{
    public interface ICommentService
    {
        public IEnumerable<Comment> GetAsync(Func<Comment, bool> predicate);
        public Comment FirstOrDefaultAsync(Func<Comment, bool> predicate);
        public void CreateAsync(Comment item);
        public void UpdateAsync(Comment item);
        public void DeleteAsync(string id);
    }
}
