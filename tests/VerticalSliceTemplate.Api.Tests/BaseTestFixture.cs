using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using VerticalSliceTemplate.Api.Common.Interfaces;

namespace VerticalSliceTemplate.Api.Tests;

public abstract class BaseTestFixture<T> : BaseTestFixture where T : class
{
    private readonly Lazy<T> _lazyInstance;

    protected readonly ILogger<T> Logger = Substitute.For<ILogger<T>>();

    protected BaseTestFixture()
    {
        _lazyInstance = new Lazy<T>(CreateInstance);
    }

    protected T Instance => _lazyInstance.Value;

    protected abstract T CreateInstance();
}

public abstract class BaseTestFixture
{
    protected readonly IApplicationDbContext ApplicationDbContextMock = Substitute.For<IApplicationDbContext>();
    protected readonly ICurrentUserService CurrentUserServiceMock = Substitute.For<ICurrentUserService>();
    protected readonly LinkGenerator LinkGeneratorMock = Substitute.For<LinkGenerator>();
    protected readonly IToDoRepository ToDoRepositoryMock = Substitute.For<IToDoRepository>();
}