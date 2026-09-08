namespace VerticalSliceTemplate.Api.Configurations;

internal static class EndpointHandlers
{
    internal static void SetupEndpointHandlers(this IServiceCollection services)
    {
        services.Scan(scan => scan.FromAssemblyOf<IEndpointHandler>()
            .AddClasses(c => c.AssignableTo<IEndpointHandler>())
            .AsSelf()
            .WithScopedLifetime());
    }
}