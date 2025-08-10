using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using CariMYS.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CariMYS.Persistence
{
    public class AutofacPersistenceModule : Autofac.Module
    {
        private readonly IConfiguration _configuration;

        public AutofacPersistenceModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
                optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));

                var context = new AppDbContext(optionsBuilder.Options);
                context.Database.Migrate();
                return context;
            }).AsSelf().InstancePerLifetimeScope();

        }

    }
}
