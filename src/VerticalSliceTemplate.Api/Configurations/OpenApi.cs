using Scalar.AspNetCore;

namespace VerticalSliceTemplate.Api.Configurations;

internal static class OpenApi
{
    internal static void UseApiDocumentation(this WebApplication app)
    {
        // Serves an OpenAPI document per discovered API version at /openapi/{documentName}.json
        app.MapOpenApi().WithDocumentPerVersion();

        app.MapScalarApiReference(options =>
        {
            var descriptions = app.DescribeApiVersions();

            for (var i = 0; i < descriptions.Count; i++)
            {
                var description = descriptions[i];
                var isDefault = i == descriptions.Count - 1;

                options.AddDocument(description.GroupName, description.GroupName, isDefault: isDefault);
            }
        });
    }
}
