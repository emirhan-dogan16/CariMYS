using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Persistence.EntityFrameworkCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Core.Persistence.EntityFrameworkCore
{
    public class UnitOfWork : IUnitOfWork
    {
        public DbContext Context { get; }

        public UnitOfWork(DbContext context)
        {
            Context = context;
        }

        public virtual void Save()
        {
            Context.SaveChanges();
        }

        public virtual async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }
    }
}
