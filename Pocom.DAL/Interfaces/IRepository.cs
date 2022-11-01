using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pocom.DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        void AddAndSave<TEntity>(TEntity entity);
        void UpdateAndSave<TEntity>(TEntity entity);
        void RemoveAndSave<TEntity>(TEntity entity);
        IList<TEntity> GetWhere<TEntity>(Func<TEntity, bool> predicate);
        TEntity FirstOrDefault<TEntity>(Func<TEntity, bool> predicate);
        int Count<TEntity>(Func<TEntity, bool> predicate);
        Task SaveAsync();
    }
}
