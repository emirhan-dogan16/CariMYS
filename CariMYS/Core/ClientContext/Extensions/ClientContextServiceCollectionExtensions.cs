using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Application.ClientContext.Extensions
{
    public static class ClientContextServiceCollectionExtensions
    {
        public static IServiceCollection AddClientContext(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            
            return services;
        }

        public static IServiceCollection AddDefaultClientContext(this IServiceCollection services)
        {
            services.AddScoped<IClientContextAccessor, ClientContextAccessor>();

            return services;
        }

        public static IServiceCollection AddCustomClientContext<T>(this IServiceCollection services)
            where T : class, IClientContextAccessor, new()
        {
            services.AddScoped<IClientContextAccessor, T>();

            return services;
        }
    }
}
