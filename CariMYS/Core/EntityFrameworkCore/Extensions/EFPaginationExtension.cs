using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Core.Persistence.EntityFrameworkCore.Extensions
{
    public static class EFPaginationExtension
    {
        public static PageResult<T> ToPage<T>(
            this IQueryable<T> query,
            int currentPage,
            int pageSize)
        {
            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var items = query
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PageResult<T>
            {
                Items = items,
                TotalCount = totalCount,
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }

        public static async Task<PageResult<T>> ToPageAsync<T>(
            this IQueryable<T> query,
            int currentPage,
            int pageSize)
        {
            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var items = await query
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageResult<T>
            {
                Items = items,
                TotalCount = totalCount,
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalPages = totalPages
            };
        }
    }
}
