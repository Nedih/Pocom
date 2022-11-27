using Microsoft.EntityFrameworkCore;
//using System.Data.Entity;
using Pocom.DAL.Interfaces;
using Pocom.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

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
        public IQueryable<TEntity> GetAll()
        {
            return this._dbSet.AsQueryable();
        }
        public IQueryable<TEntity> GetWhere(Func<TEntity, bool> predicate)
        {
            return this._dbSet.Where(predicate).AsQueryable();
        }
        public IQueryable<TEntity> GetWhere(Func<TEntity, bool> predicate, Func<TEntity, TEntity> selector)
        {
            return this._dbSet.Where(predicate).Select(selector).AsQueryable();
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
        public IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

        public IQueryable<TEntity> Sort(IQueryable<TEntity> items, string props)
        {
            throw new NotImplementedException();
        }
    }
}
