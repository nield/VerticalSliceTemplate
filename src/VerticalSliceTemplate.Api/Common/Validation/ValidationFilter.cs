using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace VerticalSliceTemplate.Api.Common.Validation;

[ExcludeFromCodeCoverage]
public static class ValidationFilter
{
    private static readonly ConcurrentDictionary<MethodInfo, ValidationDescriptor[]> _cache = [];

    public static EndpointFilterDelegate ValidationFilterFactory(
        EndpointFilterFactoryContext context,
        EndpointFilterDelegate next)
    {
        // Try to load from cache
        if (!_cache.TryGetValue(context.MethodInfo, out var descriptors))
        {
            descriptors = GetValidators(context.MethodInfo, context.ApplicationServices).ToArray();
            _cache[context.MethodInfo] = descriptors;
        }

        // If no validators → passthrough
        if (descriptors.Length == 0) return invocationContext => next(invocationContext);

        // Otherwise wrap validation logic
        return invocationContext => Validate(descriptors, invocationContext, next);
    }

    private static async ValueTask<object?> Validate(
        ValidationDescriptor[] descriptors,
        EndpointFilterInvocationContext invocationContext,
        EndpointFilterDelegate next)
    {
        foreach (var descriptor in descriptors)
        {
            var argument = invocationContext.Arguments[descriptor.ArgumentIndex];
            if (argument is not null)
            {
                ValidationResult result = await descriptor.Validator.ValidateAsync(
                    new ValidationContext<object>(argument));

                if (!result.IsValid)
                {
                    return Results.ValidationProblem(
                        result.Errors
                              .GroupBy(e => e.PropertyName)
                              .ToDictionary(
                                  g => g.Key,
                                  g => g.Select(e => e.ErrorMessage).ToArray()
                              ),
                        statusCode: (int)HttpStatusCode.BadRequest);
                }
            }
        }

        return await next(invocationContext);
    }

    private static IEnumerable<ValidationDescriptor> GetValidators(MethodInfo methodInfo, IServiceProvider services)
    {
        var parameters = methodInfo.GetParameters();

        for (int i = 0; i < parameters.Length; i++)
        {
            var parameter = parameters[i];

            if (parameter.GetCustomAttribute<ValidateAttribute>() is not null)
            {
                var validatorType = typeof(IValidator<>).MakeGenericType(parameter.ParameterType);

                if (services.GetService(validatorType) is IValidator validator)
                {
                    yield return new ValidationDescriptor
                    {
                        ArgumentIndex = i,
                        ArgumentType = parameter.ParameterType,
                        Validator = validator
                    };
                }
            }
        }
    }

    private sealed class ValidationDescriptor
    {
        public required int ArgumentIndex { get; init; }
        public required Type ArgumentType { get; init; }
        public required IValidator Validator { get; init; }
    }
}