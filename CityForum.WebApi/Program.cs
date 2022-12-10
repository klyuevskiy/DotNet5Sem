using CityForum.WebApi.AppConfiguration.ServicesExtensions;
using CityForum.WebApi.AppConfiguration.ApplicationExtensions;
using CityForum.WebApi.AppConfiguration;
using CityForum.Repository;
using CityForum.Services;
using Serilog;
using CityForum.WebApi.AppConfiguration.Middlewares;

var configuration = new ConfigurationBuilder()
.AddJsonFile("appsettings.json", optional: false)
.Build();

var builder = WebApplication.CreateBuilder(args);

builder.AddSerilogConfiguration();
builder.Services.AddDbContextConfiguration(configuration);
builder.Services.AddVersioningConfiguration();
builder.Services.AddMapperConfiguration();
builder.Services.AddControllers();
builder.Services.AddSwaggerConfiguration(configuration);
builder.Services.AddRepositoryConfiguration();
builder.Services.AddBusinessLogicConfiguration();
builder.Services.AddAuthorizationConfiguration(configuration);


var app = builder.Build();

await RepositoryInitializer.InitializeRepository(app);

app.UseSerilogConfiguration();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerConfiguration();
}

app.UseHttpsRedirection();
app.UseAuthorizationConfiguration();
app.UseMiddleware(typeof(ExceptionsMiddleware));
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