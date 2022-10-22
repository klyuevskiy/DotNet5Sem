using CityForum.WebApi.AppConfiguration.ServicesExtensions;
using CityForum.WebAPI.AppConfiguration.ApplicationExtensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddSerilogConfiguration();
builder.Services.AddVersioningConfiguration();
builder.Services.AddControllers();
builder.Services.AddSwaggerConfiguration();

var app = builder.Build();

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