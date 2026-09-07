using System.Text.Json.Serialization.Metadata;
using Asp.Versioning;
using Microsoft.AspNetCore.OpenApi;
using Scalar.AspNetCore;

namespace VerticalSliceTemplate.Api.Configurations;

internal static class ApiVersioning
{
    internal static void ConfigureVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        }).AddOpenApi(options => options.Document.AddScalarTransformers());

        // Applies to every versioned OpenAPI document.
        services.ConfigureAll<OpenApiOptions>(options => options.CreateSchemaReferenceId = CreateSchemaReferenceId);
    }

    private const string EndpointsNamespacePrefix = "VerticalSliceTemplate.Api.Endpoints.";

    // Endpoints declare nested types named Request/Response. The default schema
    // reference id is the short type name, which makes all of them collide.
    // Qualify the id with the endpoint's folder (namespace) and declaring
    // type(s) so models stay unique across versions and features - e.g.
    // V1.Todos.GetAll.Response and V2.Todos.GetAll.Response no longer clash.
    private static string? CreateSchemaReferenceId(JsonTypeInfo typeInfo)
    {
        var defaultId = OpenApiOptions.CreateDefaultSchemaReferenceId(typeInfo);
        if (defaultId is null)
        {
            return null;
        }

        var type = typeInfo.Type;

        var parts = new List<string>();

        var @namespace = type.Namespace;
        if (@namespace is not null && @namespace.StartsWith(EndpointsNamespacePrefix, StringComparison.Ordinal))
        {
            parts.Add(@namespace[EndpointsNamespacePrefix.Length..]);
        }

        var declaringTypes = new List<string>();
        for (var declaring = type.DeclaringType; declaring is not null; declaring = declaring.DeclaringType)
        {
            declaringTypes.Insert(0, declaring.Name);
        }

        parts.AddRange(declaringTypes);
        parts.Add(defaultId);

        return string.Join('.', parts);
    }
}
