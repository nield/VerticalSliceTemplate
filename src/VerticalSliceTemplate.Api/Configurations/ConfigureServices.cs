using System.Diagnostics;
using VerticalSliceTemplate.Api.Common.Services;
namespace VerticalSliceTemplate.Api.Configurations;

internal static class ConfigureServices
{
    internal static IHostApplicationBuilder AddApiServices(this IHostApplicationBuilder builder)
    {
        var config = builder.Configuration;

        builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

        builder.Services.AddSingleton(TimeProvider.System);

        builder.Services.AddHttpContextAccessor();

        builder.Services.ConfigureFluentValidator();

        builder.Services.ConfigureExceptionHandlers();

        builder.Services.ConfigureSwagger();

        builder.Services.ConfigureVersioning();

        builder.Services.ConfigureSettings(config);

        builder.Services.ConfigureCompression();

        builder.Services.ConfigureHeaderPropagation();

        builder.SetupDatabase();

        builder.Services.AddProblemDetails(options => 
            options.CustomizeProblemDetails = (context) =>
            {
                context.ProblemDetails.Instance =
                    $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";
       
                context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);

                context.ProblemDetails.Extensions.TryAdd("traceId", Activity.Current?.TraceId);
            });

        return builder;
    }
}
