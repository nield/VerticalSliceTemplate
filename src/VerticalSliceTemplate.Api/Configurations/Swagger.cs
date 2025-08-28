using VerticalSliceTemplate.Api.Swagger;

namespace VerticalSliceTemplate.Api.Configurations;

internal static class Swagger
{
    internal static void ConfigureSwagger(this IServiceCollection services, IConfiguration config)
    {
        services.AddEndpointsApiExplorer();

        services.ConfigureOptions<ConfigureSwaggerOptions>();

        services.AddSwaggerGen();
    }

    internal static void UseApiDocumentation(this WebApplication app, IConfiguration config)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            var descriptions = app.DescribeApiVersions();

            // build a swagger endpoint for each discovered API version
            foreach (var groupName in descriptions.Select(x => x.GroupName))
            {
                var url = $"/swagger/{groupName}/swagger.json";
                var name = groupName.ToUpperInvariant();
                options.SwaggerEndpoint(url, name);
            }
        });
    }
}