using System.Diagnostics.CodeAnalysis;

namespace VerticalSliceTemplate.Api.Integration.Tests.Containers;

[ExcludeFromCodeCoverage]
internal abstract class BaseContainer<TContainer>
    where TContainer : class, new()
{
    private static readonly Lazy<TContainer> SingleLazyInstance = new(() => new TContainer());

    public static TContainer Instance => SingleLazyInstance.Value;

    private readonly Lazy<IContainer> _lazyContainer;

    protected BaseContainer()
    {
        _lazyContainer = new Lazy<IContainer>(BuildContainer);
    }

    protected IContainer Container => _lazyContainer.Value;

    protected abstract IContainer BuildContainer();

    public abstract string GetConnectionString();

    public virtual async Task StartContainerAsync(CancellationToken cancellationToken)
    {
        await Container.StartAsync(cancellationToken);

        var containerRunning = IsContainerRunning(cancellationToken);

        if (!containerRunning)
        {
            throw new OperationCanceledException("The containers did not start in time");
        }
    }

    private bool IsContainerRunning(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested
            && Container.State != TestcontainersStates.Running)
        {
            Thread.Sleep(250);
        }

        return Container.State == TestcontainersStates.Running;
    }
}
