using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using VerticalSliceTemplate.Api.Common.Interfaces;

namespace VerticalSliceTemplate.Api.Tests;

public abstract class BaseTestFixture<T> : BaseTestFixture where T : class
{
    protected T Instance;

    protected readonly ILogger<T> _logger = Substitute.For<ILogger<T>>();

    protected BaseTestFixture()
    {
        Instance = CreateInstance();
    }

    protected abstract T CreateInstance();
}

public abstract class BaseTestFixture
{
    protected readonly IApplicationDbContext _applicationDbContextMock = Substitute.For<IApplicationDbContext>();
    protected readonly ICurrentUserService _currentUserServiceMock = Substitute.For<ICurrentUserService>();
    protected readonly LinkGenerator _linkGeneratorMock = Substitute.For<LinkGenerator>();
    protected readonly IToDoRepository _toDoRepositoryMock = Substitute.For<IToDoRepository>();
}