using VerticalSliceTemplate.Api.Common.ExceptionHandlers;

namespace VerticalSliceTemplate.Api.Configurations;

internal static class ExceptionHandlers
{
    /// <summary>
    /// Register exception handlers. 
    /// Registration order matters in processing of handlers.
    /// </summary>
    /// <param name="services"></param>
    internal static void ConfigureExceptionHandlers(this IServiceCollection services)
    {
        services.AddExceptionHandler<BadRequestExceptionHandler>();
        services.AddExceptionHandler<NotFoundExceptionHandler>();
        services.AddExceptionHandler<ValidationExceptionHandler>();
        services.AddExceptionHandler<UnauthorizedAccessExceptionHandler>();
        services.AddExceptionHandler<ForbiddenAccessExceptionHandler>();
        services.AddExceptionHandler<UnhandledExceptionHandler>();
    }
}