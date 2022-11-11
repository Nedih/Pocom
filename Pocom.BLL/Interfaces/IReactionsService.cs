using Pocom.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pocom.BLL.Interfaces
{
    internal interface IReactionService
    {
        public IEnumerable<Reaction> GetAsync(Func<Reaction, bool> predicate);
        public Reaction FirstOrDefaultAsync(Func<Reaction, bool> predicate);
        public void CreateAsync(Reaction item);
        public void UpdateAsync(Reaction item);
        public void DeleteAsync(string id);
    }
}
