using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pocom.DAL.Interfaces
{
    public interface IRepository
    {
        void AddAndSave<TEntity>(TEntity entity) where TEntity : class, IEntity;
        void UpdateAndSave<TEntity>(TEntity entity) where TEntity : class, IEntity;
        void RemoveAndSave<TEntity>(TEntity entity) where TEntity : class, IEntity;
        IList<TEntity> GetWhere<TEntity>(Func<TEntity, bool> predicate) where TEntity : class, IEntity;
        TEntity FirstorDefault<TEntity>(Func<TEntity, bool> predicate) where TEntity : class, IEntity;
        int Count<TEntity>(Func<TEntity, bool> predicate) where TEntity : class, IEntity;
        Task SaveAsync();
    }
}
