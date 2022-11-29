using CityForum.WebApi.MapperProfile;

namespace CityForum.WebApi.AppConfiguration.ServicesExtensions;

public static partial class ServicesExtensions
{
    /// <summary>
    /// Add services configuration
    /// </summary>
    /// <param name="builder"></param>
    public static void AddMapperConfiguration(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(PresentationProfile));
    }
}