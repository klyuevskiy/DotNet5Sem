using CityForum.Entities;
using CityForum.Entities.Models;
using CityForum.WebApi.IdentityServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CityForum.WebApi.AppConfiguration.ServicesExtensions;

public static partial class ServicesExtensions
{
    /// <summary>
    /// Add serilog configuration
    /// </summary>
    /// <param name="builder"></param>
    public static void AddAuthorizationConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        string identityUri = configuration.GetValue<string>("IdentityServer:Uri");

        services.AddIdentity<User, UserRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedAccount = false;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+!#$%&'*/=?^`{|}~";
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 1;
            })
                .AddEntityFrameworkStores<Context>()
                .AddSignInManager<SignInManager<User>>()
                .AddDefaultTokenProviders();

        services.AddIdentityServer()
            .AddInMemoryApiScopes(IdentityServerDefaults.ApiScopes)
            .AddInMemoryClients(IdentityServerDefaults.Clients)
            .AddAspNetIdentity<User>();

        services.AddAuthentication(options =>
        {
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
        {
            options.RequireHttpsMetadata = false;
            options.Authority = identityUri;
            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuerSigningKey = false,
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
            options.Audience = "api";
        });

        services.AddAuthorization();
    }
}