using System.Diagnostics;
using VerticalSliceTemplate.Api.Common.Services;
namespace VerticalSliceTemplate.Api.Configurations;

public static class ConfigureServices
{
    public static IHostApplicationBuilder AddApiServices(this IHostApplicationBuilder builder)
    {
        var config = builder.Configuration;

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

        builder.Services.AddSingleton(TimeProvider.System);

        builder.Services.AddHttpContextAccessor();

        builder.Services.ConfigureFluentValidator();

        builder.Services.ConfigureExceptionHandlers();

        builder.Services.ConfigureSwagger(config);

        builder.Services.ConfigureVersioning();

        builder.Services.ConfigureSettings(config);

        builder.Services.ConfigureCompression();

        builder.Services.ConfigureHeaderPropagation();

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
