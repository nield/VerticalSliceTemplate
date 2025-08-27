namespace VerticalSliceTemplate.Api.Common.Extensions;

public static class RouteBuilderExtensions
{
    public static RouteGroupBuilder UseMainRoute(this IEndpointRouteBuilder webApplication, int majorVersion = 1) =>
        webApplication.MapGroup("api/v{version:apiVersion}")
            .WithOpenApi()
            .WithApiVersionSet(VersionSets.GetVersionSet(majorVersion))
            .MapToApiVersion(majorVersion);

    public static IEndpointConventionBuilder MapGetRoute(this IEndpointRouteBuilder webApplication,
        string pattern, Delegate handler, int majorVersion = 1) =>
            webApplication
                .UseMainRoute(majorVersion)
                .MapGet(pattern, handler);

    public static IEndpointConventionBuilder MapPostRoute(this IEndpointRouteBuilder webApplication,
        string pattern, Delegate handler, int majorVersion = 1) =>
            webApplication
                .UseMainRoute(majorVersion)
                .MapPost(pattern, handler)
                .AddEndpointFilterFactory(ValidationFilter.ValidationFilterFactory);

    public static IEndpointConventionBuilder MapPutRoute(this IEndpointRouteBuilder webApplication,
        string pattern, Delegate handler, int majorVersion = 1) =>
            webApplication
                .UseMainRoute(majorVersion)
                .MapPut(pattern, handler)
                .AddEndpointFilterFactory(ValidationFilter.ValidationFilterFactory);

    public static IEndpointConventionBuilder MapDeleteRoute(this IEndpointRouteBuilder webApplication,
        string pattern, Delegate handler, int majorVersion = 1) =>
            webApplication
                .UseMainRoute(majorVersion)
                .MapDelete(pattern, handler)
                .AddEndpointFilterFactory(ValidationFilter.ValidationFilterFactory);
}
