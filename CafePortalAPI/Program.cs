using Autofac;
using Autofac.Extensions.DependencyInjection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using CafePortalAPI.Infrastructure.Data;
using CafePortalAPI.Application.Interfaces;
using CafePortalAPI.Infrastructure.Repositories;
using CafePortalAPI.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", builder =>
             builder.WithOrigins("http://localhost:3000")
                    .AllowAnyHeader()
                    .AllowAnyMethod());
});

// Register services with Microsoft.Extensions.DependencyInjection (standard DI container)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Set up Autofac as the Dependency Injection container
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Register MediatR services (for handling commands and queries)
//builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

// Register DbContext with connection string from appsettings.json
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Host.ConfigureContainer<ContainerBuilder>(continerBuilder =>
{
    continerBuilder.RegisterType<CafeRepository>().As<ICafeRepository>();
    continerBuilder.RegisterType<CafeService>().As<ICafeService>();
    continerBuilder.RegisterType<EmployeeService>().As<IEmployeeService>();
    continerBuilder.RegisterType<EmployeeRepository>().As<IEmployeeRepository>();
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
