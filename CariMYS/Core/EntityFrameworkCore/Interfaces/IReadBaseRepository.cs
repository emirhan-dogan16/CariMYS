using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.Persistence.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Core.Persistence.EntityFrameworkCore.Interfaces
{
    public interface IReadBaseRepository<T>
        where T : class, IEntity, new()
    {
        IEnumerable<T> GetList(Expression<Func<T, bool>>? expression = null);
        Task<IEnumerable<T>> GetListAsync(
            Expression<Func<T, bool>>? expression = null, 
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);

        //IEnumerable<T> GetPageList(int index = 1, int size = 10,
        //    Expression<Func<T, bool>>? expression = null);
        //Task<IEnumerable<T>> GetPageListAsync(int index = 1, int size = 10,
        //    Expression<Func<T, bool>>? expression = null, 
        //    Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);

        T? Get(Expression<Func<T, bool>> expression);
        Task<T?> GetAsync(
            Expression<Func<T, bool>> expression, 
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);

        Task<int> GetCountAsync(Expression<Func<T, bool>>? expression = null);
        int GetCount(Expression<Func<T, bool>>? expression = null);

        IQueryable<T> Query();

    }
}
