using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using VerticalSliceTemplate.Api.Common.Services;
using Constants = VerticalSliceTemplate.Api.Common.Constants;

namespace VerticalSliceTemplate.Api.Tests.Common.Services;

public class CurrentUserServiceTests
{
    private readonly CurrentUserService _currentUserService;
    private readonly IHttpContextAccessor _httpContextAccessorMock = Substitute.For<IHttpContextAccessor>();

    public CurrentUserServiceTests()
    {
        _currentUserService = new(_httpContextAccessorMock);
    }

    [Fact]
    public void Given_UserIdClaimExists_When_FetchingUserId_Then_ReturnsUserIdFromClaim()
    {
        var context = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity(
            [
                new(Constants.Headers.UserProfileId, "1")
            ]))
        };

        _httpContextAccessorMock.HttpContext.Returns(context);

        Assert.Equal("1", _currentUserService.UserId);
    }

    [Fact]
    public void Given_UserIdClaimDoesNotExists_When_FetchingUserId_Then_ReturnsNull()
    {
        var context = new DefaultHttpContext();

        _httpContextAccessorMock.HttpContext.Returns(context);

        Assert.Null(_currentUserService.UserId);
    }

    [Fact]
    public void Given_UserProfileIdClaimExists_When_FetchingUserId_Then_ReturnsUserProfileIdFromClaim()
    {
        var context = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity(
            [
                new(Constants.Headers.UserProfileId, "1")
            ]))
        };

        _httpContextAccessorMock.HttpContext.Returns(context);

        Assert.Equal("1", _currentUserService.UserProfileId);
    }

    [Fact]
    public void Given_UserProfileIdClaimDoesNotExists_When_FetchingUserProfileId_Then_ReturnsNull()
    {
        var context = new DefaultHttpContext();

        _httpContextAccessorMock.HttpContext.Returns(context);

        Assert.Null(_currentUserService.UserProfileId);
    }

    [Fact]
    public void Given_CorrelationIdClaimExists_When_FetchingCorrelationId_Then_ReturnsCorrelationIdFromClaim()
    {
        var correlationId = Guid.NewGuid().ToString();

        var context = new DefaultHttpContext();
        context.Request.Headers[Constants.Headers.CorrelationId] = correlationId;

        _httpContextAccessorMock.HttpContext.Returns(context);

        Assert.Equal(correlationId, _currentUserService.CorrelationId);
    }

    [Fact]
    public void Given_CorrelationIdClaimDoesNotExists_When_FetchingCorrelationId_Then_ReturnsNull()
    {
        var context = new DefaultHttpContext();

        _httpContextAccessorMock.HttpContext.Returns(context);

        Assert.Null(_currentUserService.CorrelationId);
    }

    [Fact]
    public void Given_AuthorizationClaimExists_When_FetchingToken_Then_ReturnsAuthorizationFromClaim()
    {
        var context = new DefaultHttpContext();

        context.Request.Headers.Append(Constants.Headers.Authorization, "token");

        _httpContextAccessorMock.HttpContext.Returns(context);

        Assert.Equal("token", _currentUserService.Token);
    }

    [Fact]
    public void GivenAuthorizationClaimDoesNotExists_When_FetchingToken_Then_ReturnsNull()
    {
        var context = new DefaultHttpContext();

        _httpContextAccessorMock.HttpContext.Returns(context);

        Assert.Null(_currentUserService.Token);
    }
}