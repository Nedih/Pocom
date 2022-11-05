using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pocom.DAL.Entities;

namespace Pocom.DAL.Interfaces
{
<<<<<<< Updated upstream
    public interface IRepository
    {
        void AddAndSave<TEntity>(TEntity entity) where TEntity : class, IEntity;
        void UpdateAndSave<TEntity>(TEntity entity) where TEntity : class, IEntity;
        void RemoveAndSave<TEntity>(TEntity entity) where TEntity : class, IEntity;
        IList<TEntity> GetWhere<TEntity>(Func<TEntity, bool> predicate) where TEntity : class, IEntity;
        TEntity FirstorDefault<TEntity>(Func<TEntity, bool> predicate) where TEntity : class, IEntity;
        int Count<TEntity>(Func<TEntity, bool> predicate) where TEntity : class, IEntity;
=======
    public interface IRepository<TEntity> where TEntity : class
    {
        void AddAndSave(TEntity entity);
        void UpdateAndSave(TEntity entity);
        void RemoveAndSave(TEntity entity);
        IList<TEntity> GetWhere(Func<TEntity, bool> predicate);
        TEntity FirstOrDefault(Func<TEntity, bool> predicate);
        int Count(Func<TEntity, bool> predicate);
>>>>>>> Stashed changes
        Task SaveAsync();
    }
}
