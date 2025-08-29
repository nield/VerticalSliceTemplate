using Types = NetArchTest.Rules.Types;

namespace VerticalSliceTemplate.Api.Tests;

public class ArchitectureTests
{
    [Fact]
    public void Endpoints_ShouldNotHaveDependencyOn_Infrastructure()
    {
        var result = Types.InCurrentDomain()
            .That()
            .ResideInNamespace("VerticalSliceTemplate.Api.Endpoints")
            .ShouldNot()
            .HaveDependencyOn("VerticalSliceTemplate.Api.Infrastructure")
            .GetResult()
            .IsSuccessful;

        Assert.True(result);
    }

    [Fact]
    public void Endpoints_ShouldBeSealed()
    {
        var result = Types.InCurrentDomain()
            .That()
            .ResideInNamespace("VerticalTemplate.Api.Endpoints")
            .Should()
            .BeSealed()
            .GetResult()
            .IsSuccessful;

        Assert.True(result);
    }
}
