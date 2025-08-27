using Serilog;

namespace VerticalSliceTemplate.Api.Configurations;

internal static class Logging
{
    internal static void ConfigureLogging(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();

        builder.Host.UseSerilog((context, services, configuration)
                                    => configuration.ReadFrom.Configuration(context.Configuration));
    }

    internal static void UseLogging(this WebApplication app)
    {
        app.UseSerilogRequestLogging();
    }
}
