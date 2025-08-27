using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using VerticalSliceTemplate.Api.Swagger;

namespace VerticalSliceTemplate.Api.Configurations;

internal static class Swagger
{
    internal static void ConfigureSwagger(this IServiceCollection services, IConfiguration config)
    {
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

        services.AddSwaggerGen(options => 
        {
            // Use full type name (with namespace) as schemaId
            options.CustomSchemaIds(type =>
            {
                // Use full name, but replace '+' (nested class separator) with '_'
                return type.FullName!.Replace("+", "_");
            });

            options.OperationFilter<SwaggerDefaultValues>();
        });
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

            options.OAuthClientId(config["AuthorityOptions:Client"]);
        });
    }
}