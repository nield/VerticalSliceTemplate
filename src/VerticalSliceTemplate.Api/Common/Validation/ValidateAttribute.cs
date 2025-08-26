using System.Diagnostics.CodeAnalysis;

namespace VerticalSliceTemplate.Api.Common.Validation;

[ExcludeFromCodeCoverage]
[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
public class ValidateAttribute : Attribute
{

}