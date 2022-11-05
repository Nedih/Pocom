using Microsoft.EntityFrameworkCore;
using Pocom.DAL.Interfaces;
using Pocom.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pocom.DAL.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private DbContext _context;
        private DbSet<TEntity> _dbSet;

        public Repository(PocomContext context)
        {
            this._context = context;
            this._dbSet = context.Set<TEntity>();
        }

        public TEntity FirstOrDefault(Func<TEntity, bool> predicate)
        {
            return this._dbSet.FirstOrDefault(predicate);
        }
        public IList<TEntity> GetWhere(Func<TEntity, bool> predicate)
        {
            return this._dbSet.Where(predicate).ToList();
        }
        public int Count(Func<TEntity, bool> predicate)
        {
            return this._dbSet.Count(predicate);
        }
        public void AddAndSave(TEntity entity)
        {
            this._dbSet.Add(entity);
            this._context.SaveChanges();
        }
        public void RemoveAndSave(TEntity entity)
        {
            this._dbSet.Remove(entity);
            this._context.SaveChanges();
        }
        public void UpdateAndSave(TEntity entity)
        {
            this._context.Entry(entity).State = EntityState.Modified;
            this._context.SaveChanges();
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
