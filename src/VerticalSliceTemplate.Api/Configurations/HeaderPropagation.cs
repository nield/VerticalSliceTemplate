using static VerticalSliceTemplate.Api.Common.Constants;
using VerticalSliceTemplate.Api.Common.Extensions;

namespace VerticalSliceTemplate.Api.Configurations;

public static class HeaderPropagation
{
    public static void ConfigureHeaderPropagation(this IServiceCollection services)
    {
        services.AddHeaderPropagation(options =>
            options.Headers.Add(Headers.CorrelationId, context => context.HttpContext.GetCorrelationId()));
    }
}
