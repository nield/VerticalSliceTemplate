using FluentValidation;
using System.Reflection;

namespace VerticalSliceTemplate.Api.Configurations;

public static class FluentValidator
{
    public static void ConfigureFluentValidator(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblies(
        [
            Assembly.GetExecutingAssembly()
        ], ServiceLifetime.Singleton);
    }
}
