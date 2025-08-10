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
    public class EFReadBaseRepository<TEntity, TContext> : IReadBaseRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext
    {
        public EFReadBaseRepository(TContext context)
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
    }
}
