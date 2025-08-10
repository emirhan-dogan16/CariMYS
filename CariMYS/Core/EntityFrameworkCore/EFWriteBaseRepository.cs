using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.Persistence.Entities;
using Core.Persistence.EntityFrameworkCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Core.Persistence.EntityFrameworkCore
{
    public class EFWriteBaseRepository<TEntity, TContext> : IWriteBaseRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext
    {
        public EFWriteBaseRepository(TContext context)
        {
            Context = context;
        }

        protected TContext Context { get; }

        public TEntity Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
            return entity;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
            return entity;
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await Context.Set<TEntity>().AddRangeAsync(entities);
        }

        public void Delete(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = Context.Set<TEntity>().Where(predicate).ToList();
            Context.Set<TEntity>().RemoveRange(entities);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = await Context.Set<TEntity>().Where(predicate).ToListAsync();
            Context.Set<TEntity>().RemoveRange(entities);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }

        public async Task DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
            await Task.CompletedTask;
        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return Context.SaveChangesAsync();
        }

        public TEntity Update(TEntity entity)
        {
            Context.Set<TEntity>().Update(entity);
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            Context.Set<TEntity>().Update(entity);
            await Task.CompletedTask;
            return entity;
        }
    }
}
