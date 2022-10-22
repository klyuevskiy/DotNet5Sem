using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace CityForum.WebAPI.AppConfiguration.ApplicationExtensions
{
    /// <summary>
    /// Application builder extensions
    /// </summary>
    public static partial class AppExtensions
    {
        /// <summary>
        /// Use swagger configuration
        /// </summary>
        /// <param name="app"></param>
        public static void UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                var provider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"{description.GroupName}/swagger.json", description.GroupName);
                };
            });
        }
    }
}