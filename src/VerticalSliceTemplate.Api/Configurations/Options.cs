using VerticalSliceTemplate.Api.Common.Settings;

namespace VerticalSliceTemplate.Api.Configurations;

public static class Options
{
    public static void ConfigureSettings(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<AppSettings>(config);
    }
}
