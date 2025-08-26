﻿namespace VerticalSliceTemplate.Api.Configurations;

public static class ApiEndpoints
{
    public static void AddApiEndpoints(this IEndpointRouteBuilder app)
    {
        var endpointTypes = typeof(IEndpoint).Assembly
            .GetTypes()
            .Where(t => typeof(IEndpoint).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

        foreach (var type in endpointTypes)
        {
            var endpoint = (IEndpoint)Activator.CreateInstance(type)!;
            endpoint.AddRoute(app);
        }
    }
}
