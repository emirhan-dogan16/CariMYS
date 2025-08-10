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
    public class EFEntityBaseRepository<TEntity, TContext> : IWriteBaseRepository<TEntity>, IReadBaseRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext
    {
        public EFEntityBaseRepository(TContext context)
        {
            Context = context;
        }

        protected TContext Context { get; }

        public TEntity? Get(Expression<Func<TEntity, bool>> expression)
        {
            return Context.Set<TEntity>().FirstOrDefault(expression);
        }

        public async Task<TEntity?> GetAsync(
            Expression<Func<TEntity, bool>> expression,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>().AsQueryable();

            if (include != null)
            {
                query = include(query);
            }

            return await query.FirstOrDefaultAsync(expression);
        }

        public int GetCount(Expression<Func<TEntity, bool>>? expression = null)
        {
            return expression == null
                ? Context.Set<TEntity>().Count()
                : Context.Set<TEntity>().Count(expression);
        }

        public async Task<int> GetCountAsync(Expression<Func<TEntity, bool>>? expression = null)
        {
            return expression == null
                ? await Context.Set<TEntity>().CountAsync()
                : await Context.Set<TEntity>().CountAsync(expression);

        }

        public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>>? expression = null)
        {
            return expression == null
                ? Context.Set<TEntity>().AsNoTracking()
                : Context.Set<TEntity>().Where(expression).AsNoTracking();
        }

        public async Task<IEnumerable<TEntity>> GetListAsync(
            Expression<Func<TEntity, bool>>? expression = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>().AsQueryable();

            if (include != null)
            {
                query = include(query);
            }

            if (expression != null)
            {
                query = query.Where(expression);
            }

            return await query.ToListAsync();
        }

        public IQueryable<TEntity> Query()
        {
            return Context.Set<TEntity>();
        }

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
