using CityForum.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CityForum.Repository;

public static partial class ServicesExtensions
{
    public static void AddRepositoryConfiguration(this IServiceCollection services)
    {
        services.AddScoped<DbContext, Context>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    }
}