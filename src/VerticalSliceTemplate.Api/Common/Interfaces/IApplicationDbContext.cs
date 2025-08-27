namespace VerticalSliceTemplate.Api.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<ToDoItem> TodoItems { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}