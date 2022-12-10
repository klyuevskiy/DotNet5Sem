namespace CityForum.WebApi.AppConfiguration.ApplicationExtensions;

public static partial class AppExtensions
{
    /// <summary>
    /// Use serilog configuration
    /// </summary>
    /// <param name="app"></param>
    public static void UseAuthorizationConfiguration(this IApplicationBuilder app)
    {
        app.UseIdentityServer();
        app.UseAuthentication();
        app.UseAuthorization();
    }
}