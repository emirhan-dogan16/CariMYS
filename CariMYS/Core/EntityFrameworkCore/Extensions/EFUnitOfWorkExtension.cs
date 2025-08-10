using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Persistence.EntityFrameworkCore.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Persistence.EntityFrameworkCore.Extensions
{
    public static class EFUnitOfWorkExtension
    {

        public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
