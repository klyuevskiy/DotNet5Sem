using CityForum.Services.MapperProfile;
using Microsoft.Extensions.DependencyInjection;
using CityForum.Services.Abstract;
using CityForum.Services.Impelementation;

namespace CityForum.Services;

public static partial class ServicesExtensions
{
    public static void AddBusinessLogicConfiguration(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ServicesProfile));

        //services
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITopicService, TopicService>();
        services.AddScoped<IMessageService, MessageService>();
    }
}