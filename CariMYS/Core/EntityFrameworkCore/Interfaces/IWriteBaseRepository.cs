using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;
using Core.Persistence.Entities;
using System.Linq.Expressions;

namespace Core.Persistence.EntityFrameworkCore.Interfaces
{
    public interface IWriteBaseRepository<T>
        where T : class, IEntity, new()
    {
        T Add(T entity);
        T Update(T entity);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> predicate);

        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task DeleteAsync(Expression<Func<T, bool>> predicate);

        void AddRange(IEnumerable<T> entities);
        void DeleteRange(IEnumerable<T> entities);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task DeleteRangeAsync(IEnumerable<T> entities);

        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
