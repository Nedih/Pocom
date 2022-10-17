using Microsoft.EntityFrameworkCore;
using Pocom.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pocom.DAL.Repositories
{
    public class Repository : IRepository
    {
        private DbContext context;

        public Repository(PocomContext context)
        {
            this.context = context;
        }
        
        TEntity IRepository.FirstorDefault<TEntity>(Func<TEntity, bool> predicate)
        {
            return this.context.Set<TEntity>().FirstOrDefault(predicate);
        }
        IList<TEntity> IRepository.GetWhere<TEntity>(Func<TEntity, bool> predicate)
        {
            return this.context.Set<TEntity>().Where(predicate).ToList();
        }
        int IRepository.Count<TEntity>(Func<TEntity, bool> predicate)
        {
            return this.context.Set<TEntity>().Count(predicate);
        }
        void IRepository.AddAndSave<TEntity>(TEntity entity)
        {
            this.context.Set<TEntity>().Add(entity);
            this.context.SaveChanges();
        }
        void IRepository.RemoveAndSave<TEntity>(TEntity entity)
        {
            this.context.Set<TEntity>().Remove(entity);
            this.context.SaveChanges();
        }
        void IRepository.UpdateAndSave<TEntity>(TEntity entity)
        {
            this.context.Entry(entity).State = EntityState.Modified;
            this.context.SaveChanges();
        }
        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
