using CityForum.WebApi.AppConfiguration.ServicesExtensions;
using CityForum.WebAPI.AppConfiguration.ApplicationExtensions;
using CityForum.Entities;
using CityForum.Repository;
using Microsoft.EntityFrameworkCore;
using Serilog;

var configuration = new ConfigurationBuilder()
.AddJsonFile("appsettings.json")
.Build();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddSerilogConfiguration();
builder.Services.AddDbContextConfiguration(configuration);
builder.Services.AddVersioningConfiguration();
builder.Services.AddControllers();
builder.Services.AddSwaggerConfiguration();

builder.Services.AddScoped<DbContext, Context>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

var app = builder.Build();

app.UseSerilogConfiguration();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerConfiguration();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

try
{
    Log.Information("Application starting...");

    app.Run();
}
catch (Exception ex)
{
    Log.Error("Application finished with error {error}", ex);
}
finally
{
    Log.Information("Application stopped");
    Log.CloseAndFlush();
}