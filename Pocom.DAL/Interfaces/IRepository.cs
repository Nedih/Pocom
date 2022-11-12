using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pocom.DAL.Entities;

namespace Pocom.DAL.Interfaces
{
        void AddAndSave(TEntity entity);
        void UpdateAndSave(TEntity entity);
        void RemoveAndSave(TEntity entity);
        IList<TEntity> GetWhere(Func<TEntity, bool> predicate);
        TEntity FirstOrDefault(Func<TEntity, bool> predicate);
        int Count(Func<TEntity, bool> predicate);
        Task SaveAsync();
    }
}
