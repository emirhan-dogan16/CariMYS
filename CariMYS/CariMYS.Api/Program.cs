using Autofac;
using Autofac.Extensions.DependencyInjection;
using CariMYS.Persistence;
using Core.Application.ClientContext.Extensions;
using Core.Translation.Extensions;
using Core.Persistence.EntityFrameworkCore.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new AutofacPersistenceModule(builder.Configuration));
});

builder.Services.AddSupportedLanguages(builder.Configuration);
builder.Services.AddClientContext().AddDefaultClientContext();

builder.Services.AddUnitOfWork();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
