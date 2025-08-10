using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;

namespace Core.Persistence.EntityFrameworkCore.Interfaces
{
    public interface IUnitOfWork 
    {
        void Save();
        Task SaveAsync();
    }
}
