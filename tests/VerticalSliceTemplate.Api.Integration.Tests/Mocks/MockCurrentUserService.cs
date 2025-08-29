using System.Diagnostics.CodeAnalysis;

namespace VerticalSliceTemplate.Api.Integration.Tests.Mocks;

[ExcludeFromCodeCoverage]
public class MockCurrentUserService : ICurrentUserService
{
    public string UserProfileId => "1";

    public string CorrelationId => "1";

    public string Token => "1";

    public string? UserId => "1";
}