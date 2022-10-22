using Serilog;

namespace CityForum.WebApi.AppConfiguration.ServicesExtensions
{
    public static partial class ServicesExtensions
    {
        /// <summary>
        /// Add serilog configuration
        /// </summary>
        /// <param name="builder"></param>
        public static void AddSerilogConfiguration(this WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog((context, loggerConfiguration) =>
            {
                loggerConfiguration
                .Enrich.WithCorrelationId()
                .ReadFrom.Configuration(context.Configuration);
            });

            builder.Services.AddHttpContextAccessor();
        }
    }
}