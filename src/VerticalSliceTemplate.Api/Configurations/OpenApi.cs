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
            options.DefaultOpenAllTags = true;
            
            options.SortTagsAlphabetically();
            
            var descriptions = app.DescribeApiVersions();

            foreach (var description in descriptions)
            {
                var isDefault = description.GroupName == "v1";

                options.AddDocument(description.GroupName, description.GroupName, isDefault: isDefault);
            }
        });
    }
}
