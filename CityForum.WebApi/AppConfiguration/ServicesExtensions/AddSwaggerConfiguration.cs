using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;

namespace CityForum.WebApi.AppConfiguration.ServicesExtensions
{
    public static partial class ServicesExtensions
    {
        private static string AppTitle = "City Forum Web API";

        /// <summary>
        /// Add swagger settings
        /// </summary>
        /// <param name="services"></param>
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                // note: need a temporary service provider here because one has not been created yet
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                // add a swagger document for each discovered API version
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(description.GroupName, new OpenApiInfo
                    {
                        Version = description.GroupName,
                        Title = $"{AppTitle}",
                    });
                }

                options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                //add xml file for api document
                var xmlFile = $"api.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
        }
    }
}