using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace CityForum.WebApi.AppConfiguration.ServicesExtensions;

public static partial class ServicesExtensions
{
    private static string AppTitle = "CityForum Web API";

    /// <summary>
    /// Add swagger settings
    /// </summary>
    /// <param name="services"></param>
    public static void AddSwaggerConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        string identityUri = configuration.GetValue<string>("IdentityServer:Uri");
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

            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Name = JwtBearerDefaults.AuthenticationScheme,
                Type = SecuritySchemeType.OAuth2,
                Scheme = "oauth2",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Flows = new OpenApiOAuthFlows
                {
                    Password = new OpenApiOAuthFlow
                    {
                        TokenUrl = new Uri($"{identityUri}/connect/token")
                    },
                }
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "oauth2"
                            },
                        },
                        new List<string>()
                    }
            });
        });
    }
}